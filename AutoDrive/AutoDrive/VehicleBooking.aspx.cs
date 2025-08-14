using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutoDrive
{
    public partial class VehicleBooking : System.Web.UI.Page
    {
        private string connectionString = "Data Source=DESKTOP-00GRLI4;Database=web_application;Integrated Security=True;trustservercertificate=true";
        private int currentCarID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            cvPickupDate.ValueToCompare = DateTime.Now.ToString("yyyy-MM-dd");

            if (!IsPostBack)
            {
                if (Request.QueryString["CarID"] != null && int.TryParse(Request.QueryString["CarID"], out currentCarID))
                {
                    hfCarID.Value = currentCarID.ToString();
                    LoadCarDetails(currentCarID);
                }
                else
                {
                    ltMessage.Text = "<div class='alert alert-danger'>No car selected for booking. Please go back to <a href='AvailableCars.aspx'>AvailableCars</a>.</div>";
                    DisableBookingForm();
                }
            }
            else
            {
                if (!int.TryParse(hfCarID.Value, out currentCarID))
                {
                    ltMessage.Text = "<div class='alert alert-danger'>Invalid car ID in session. Please select a car again.</div>";
                    DisableBookingForm();
                }
            }
        }

        private void LoadCarDetails(int carID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetCarDetailsForBooking", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CarID", carID);
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                bool isAvailable = Convert.ToBoolean(reader["IsAvailable"]);
                                if (!isAvailable)
                                {
                                    ltMessage.Text = "<div class='alert alert-warning'>This car is currently not available for booking. It might have been recently booked or taken offline.</div>";
                                    DisableBookingForm();
                                    return;
                                }

                                ltMake.Text = reader["Make"].ToString();
                                ltModel.Text = reader["Model"].ToString();
                                ltYear.Text = reader["Year"].ToString();
                                ltColor.Text = reader["Color"].ToString();
                                ltMileage.Text = Convert.ToInt32(reader["Mileage"]).ToString("N0", CultureInfo.InvariantCulture);
                                ltTransmission.Text = reader["Transmission"].ToString();
                                ltFuelType.Text = reader["FuelType"].ToString();
                                ltDescription.Text = reader["Description"].ToString();
                                
                                decimal originalPrice = Convert.ToDecimal(reader["Price"]);
                                ltOriginalPrice.Text = originalPrice.ToString("C", CultureInfo.InvariantCulture);

                                decimal dailyRate = originalPrice / 150m;
                                hfDailyRate.Value = dailyRate.ToString(CultureInfo.InvariantCulture);
                                ltDailyRate.Text = dailyRate.ToString("C", CultureInfo.InvariantCulture) + " / day";
                            }
                            else
                            {
                                ltMessage.Text = "<div class='alert alert-danger'>Car not found. Please select a valid car from <a href='AvailableCars.aspx'>Available Cars</a>.</div>";
                                DisableBookingForm();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ltMessage.Text = "<div class='alert alert-danger'>Error loading car details: " + ex.Message + "</div>";
                DisableBookingForm();
            }
        }

        private void DisableBookingForm()
        {
            txtPickupDate.Enabled = false;
            txtReturnDate.Enabled = false;
            btnBookCar.Enabled = false;
            ltTotalCost.Text = "N/A";
            hfCarID.Value = "0";
        }

        protected void btnBookCar_Click(object sender, EventArgs e)
        {
            ltMessage.Text = "";

            if (!Page.IsValid)
            {
                return;
            }

            int userID = (int)Session["UserID"];
            int carID = Convert.ToInt32(hfCarID.Value);

            if (carID == 0)
            {
                ltMessage.Text = "<div class='alert alert-danger'>No valid car selected for booking.</div>";
                return;
            }

            DateTime pickupDate;
            DateTime returnDate;
            decimal dailyRate;

            if (!DateTime.TryParse(txtPickupDate.Text, out pickupDate))
            {
                ltMessage.Text = "<div class='alert alert-danger'>Invalid Pickup Date format.</div>";
                return;
            }
            if (!DateTime.TryParse(txtReturnDate.Text, out returnDate))
            {
                ltMessage.Text = "<div class='alert alert-danger'>Invalid Return Date format.</div>";
                return;
            }
            if (!decimal.TryParse(hfDailyRate.Value, out dailyRate) || dailyRate <= 0)
            {
                ltMessage.Text = "<div class='alert alert-danger'>Invalid daily rate calculation. Cannot proceed with booking.</div>";
                return;
            }

            if (pickupDate < DateTime.Today)
            {
                ltMessage.Text = "<div class='alert alert-danger'>Pickup date cannot be in the past.</div>";
                return;
            }
            if (returnDate <= pickupDate)
            {
                ltMessage.Text = "<div class='alert alert-danger'>Return date must be after pickup date.</div>";
                return;
            }

            TimeSpan duration = returnDate - pickupDate;
            int numberOfDays = (int)Math.Ceiling(duration.TotalDays);
            decimal totalCost = numberOfDays * dailyRate;

            ltTotalCost.Text = totalCost.ToString("C", CultureInfo.InvariantCulture);

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();
                    try
                    {
                        // Check car availability using the same stored procedure used for loading details
                        SqlCommand checkCmd = new SqlCommand("usp_GetCarDetailsForBooking", con, transaction);
                        checkCmd.CommandType = CommandType.StoredProcedure;
                        checkCmd.Parameters.AddWithValue("@CarID", carID);
                        SqlDataReader reader = checkCmd.ExecuteReader();

                        bool currentIsAvailable = false;
                        if (reader.Read())
                        {
                            currentIsAvailable = Convert.ToBoolean(reader["IsAvailable"]);
                        }
                        reader.Close(); // Always close reader before new command on same connection

                        if (!currentIsAvailable)
                        {
                            transaction.Rollback();
                            ltMessage.Text = "<div class='alert alert-warning'>This car is no longer available. It might have been recently booked or taken offline.</div>";
                            DisableBookingForm();
                            return;
                        }

                        // 2. Call stored procedure for inserting booking
                        SqlCommand bookingCmd = new SqlCommand("usp_InsertBooking", con, transaction);
                        bookingCmd.CommandType = CommandType.StoredProcedure;
                        bookingCmd.Parameters.AddWithValue("@UserID", userID);
                        bookingCmd.Parameters.AddWithValue("@CarID", carID);
                        bookingCmd.Parameters.AddWithValue("@PickupDate", pickupDate);
                        bookingCmd.Parameters.AddWithValue("@ReturnDate", returnDate);
                        bookingCmd.Parameters.AddWithValue("@TotalCost", totalCost); // <--- ADDED THIS LINE!
                        bookingCmd.ExecuteNonQuery();

                        // 3. Call stored procedure for updating car availability
                        SqlCommand updateCarCmd = new SqlCommand("usp_UpdateCarAvailability", con, transaction);
                        updateCarCmd.CommandType = CommandType.StoredProcedure;
                        updateCarCmd.Parameters.AddWithValue("@CarID", carID);
                        updateCarCmd.Parameters.AddWithValue("@IsAvailable", 0); // Set to 0 (false) for booked
                        updateCarCmd.ExecuteNonQuery();

                        transaction.Commit();
                        ltMessage.Text = "<div class='alert alert-success'>Booking confirmed successfully! Total Cost: " + totalCost.ToString("C", CultureInfo.InvariantCulture) + "<br/> You will be redirected to My Bookings page shortly.</div>";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "setTimeout(function(){ window.location.href='MyBookings.aspx'; }, 3000);", true);
                        DisableBookingForm();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ltMessage.Text = "<div class='alert alert-danger'>Error processing booking: " + ex.Message + "</div>";
                    }
                }
            }
            catch (Exception ex)
            {
                ltMessage.Text = "<div class='alert alert-danger'>Database connection error: " + ex.Message + "</div>";
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AvailableCars.aspx");
        }
    }
}