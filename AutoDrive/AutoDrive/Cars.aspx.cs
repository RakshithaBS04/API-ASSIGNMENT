using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutoDrive
{
    public partial class Cars : Page
    {
        private string connectionString = "Data Source=DESKTOP-00GRLI4;Database=web_application;Integrated Security=True;trustservercertificate=true";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCars();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Cars 
                                (Make, Model, Year, Color, Mileage, Transmission, FuelType, Price, Description, IsAvailable, AddedDate) 
                                VALUES 
                                (@Make, @Model, @Year, @Color, @Mileage, @Transmission, @FuelType, @Price, @Description, @IsAvailable, GETDATE())";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Make", txtMake.Text);
                cmd.Parameters.AddWithValue("@Model", txtModel.Text);
                cmd.Parameters.AddWithValue("@Year", Convert.ToInt32(txtYear.Text));
                cmd.Parameters.AddWithValue("@Color", txtColor.Text);
                cmd.Parameters.AddWithValue("@Mileage", Convert.ToInt32(txtMileage.Text));
                cmd.Parameters.AddWithValue("@Transmission", txtTransmission.Text);
                cmd.Parameters.AddWithValue("@FuelType", txtFuel.Text);
                cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text));
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                cmd.Parameters.AddWithValue("@IsAvailable", chkAvailable.Checked);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            Clear();
            LoadCars();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Cars SET 
                                Make=@Make, Model=@Model, Year=@Year, Color=@Color, Mileage=@Mileage,
                                Transmission=@Transmission, FuelType=@FuelType, Price=@Price,
                                Description=@Description, IsAvailable=@IsAvailable, UpdatedAt=GETDATE()
                                WHERE CarID=@CarID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CarID", Convert.ToInt32(hfCarID.Value));
                cmd.Parameters.AddWithValue("@Make", txtMake.Text);
                cmd.Parameters.AddWithValue("@Model", txtModel.Text);
                cmd.Parameters.AddWithValue("@Year", Convert.ToInt32(txtYear.Text));
                cmd.Parameters.AddWithValue("@Color", txtColor.Text);
                cmd.Parameters.AddWithValue("@Mileage", Convert.ToInt32(txtMileage.Text));
                cmd.Parameters.AddWithValue("@Transmission", txtTransmission.Text);
                cmd.Parameters.AddWithValue("@FuelType", txtFuel.Text);
                cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text));
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                cmd.Parameters.AddWithValue("@IsAvailable", chkAvailable.Checked);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            Clear();
            LoadCars();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Cars WHERE CarID=@CarID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CarID", Convert.ToInt32(hfCarID.Value));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            Clear();
            LoadCars();
        }

        protected void gvCars_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvCars.SelectedRow;

            hfCarID.Value = row.Cells[1].Text;
            txtMake.Text = row.Cells[2].Text;
            txtModel.Text = row.Cells[3].Text;
            txtYear.Text = row.Cells[4].Text;
            txtColor.Text = row.Cells[5].Text;
            txtMileage.Text = row.Cells[6].Text;
            txtTransmission.Text = row.Cells[7].Text;
            txtFuel.Text = row.Cells[8].Text;
            txtPrice.Text = row.Cells[9].Text;
            txtDescription.Text = row.Cells[10].Text;
            chkAvailable.Checked = (row.Cells[11].Text == "True");
        }

        private void LoadCars()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Cars";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                gvCars.DataSource = dt;
                gvCars.DataBind();
            }
        }

        private void Clear()
        {
            txtMake.Text = txtModel.Text = txtYear.Text = txtColor.Text =
            txtMileage.Text = txtTransmission.Text = txtFuel.Text =
            txtPrice.Text = txtDescription.Text = string.Empty;

            chkAvailable.Checked = true;
            hfCarID.Value = string.Empty;
        }
    }
}
