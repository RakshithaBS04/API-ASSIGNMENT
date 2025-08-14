using System;
using System.Web.UI;

namespace AutoDrive
{
    public partial class UserHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || Session["UserRole"] == null || (string)Session["UserRole"] != "User")
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
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