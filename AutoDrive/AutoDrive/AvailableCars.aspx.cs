using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace AutoDrive
{
    public partial class AvailableCars : System.Web.UI.Page
    {
        private string connectionString = "Data Source=DESKTOP-00GRLI4;Database=web_application;Integrated Security=True;trustservercertificate=true";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAvailableCars();
            }
        }
        private void BindAvailableCars()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_FetchAvailableCars", con))  // 1. Stored Procedure
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            rptCars.DataSource = dt;
                            rptCars.DataBind();
                            ltMessage.Text = ""; // Clear any previous messages
                        }
                        else
                        {
                            rptCars.DataSource = null; // No data
                            rptCars.DataBind();
                            ltMessage.Text = "<div class='alert alert-info text-center'>No cars currently available for booking. Please check back later!</div>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ltMessage.Text = "<div class='alert alert-danger'>Error loading available cars: " + ex.Message + "</div>";
            }
        }

        protected void rptCars_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "BookCar")
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("Login.aspx?returnUrl=" + Server.UrlEncode(Request.RawUrl));
                }
                else
                {
                    int carID = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("VehicleBooking.aspx?CarID=" + carID);
                }
            }
        }
    }
}