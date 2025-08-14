using System;
using System.Web.UI;

namespace AutoDrive
{
    public partial class AdminHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is logged in and has the Admin role
            if (Session["UserID"] == null || Session["UserRole"] == null || (string)Session["UserRole"] != "Admin")
            {
                Response.Redirect("Login.aspx"); // Redirect to login if not authorized
            }
            else
            {
                // If authorization passes, display admin information
                if (!IsPostBack) 
                {
                    ltUsername.Text = Session["Username"] as string;
                    ltUserID.Text = Session["UserID"].ToString();
                    ltUserRole.Text = Session["UserRole"] as string;
                }
            }
        }
    }
}