using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace AutoDrive
{
    public partial class Login : System.Web.UI.Page
    {
        private string connectionString = "Data Source=DESKTOP-00GRLI4;Database=web_application;Integrated Security=True;trustservercertificate=true";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Handle Logout request FIRST and clear the query string
            if (Request.QueryString["logout"] == "true")
            {
                Session.Clear();
                Session.Abandon();
                Response.Redirect("Login.aspx", false); // false to prevent ThreadAbortException
                Context.ApplicationInstance.CompleteRequest(); // End the request
                return;
            }

            // Redirect if already authenticated based on stored role
            if (Session["UserID"] != null)
            {
                string userRole = Session["UserRole"] as string;
                if (userRole == "Admin")
                {
                    Response.Redirect("AdminHome.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else // Default to UserHome for any other role (e.g., "User")
                {
                    Response.Redirect("UserHome.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ltMessage.Text = ""; // Clear previous messages

            if (!Page.IsValid)
            {
                return;
            }

            string usernameOrEmail = txtUsername.Text.Trim();
            string password = txtPassword.Text; // Plain text password entered by user

            if (string.IsNullOrWhiteSpace(usernameOrEmail) || string.IsNullOrWhiteSpace(password))
            {
                ltMessage.Text = "<div class='alert alert-danger'>Please enter both username/email and password.</div>";
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query to fetch user by username/email and retrieve stored plain-text password and Role
                    string query = "SELECT UserID, Username, PasswordHash, Role FROM Users WHERE (Username = @UsernameOrEmail OR Email = @UsernameOrEmail)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UsernameOrEmail", usernameOrEmail);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedPassword = reader["PasswordHash"].ToString(); // Retrieve the stored plain-text password
                                string dbUsername = reader["Username"].ToString(); // Get username for session
                                string userDbRole = reader["Role"].ToString(); // Get role for session and redirection
                                int userID = Convert.ToInt32(reader["UserID"]);

                                // --- IMPORTANT: Directly comparing plain text passwords ---
                                if (password.Equals(storedPassword))
                                {
                                    // Authentication successful
                                    Session["UserID"] = userID;
                                    Session["Username"] = dbUsername;
                                    Session["UserRole"] = userDbRole; // Store the role in session

                                    reader.Close(); // Close reader before executing another command on same connection

                                    // Update last login time
                                    string updateLoginTimeQuery = "UPDATE Users SET LastLogin = GETDATE() WHERE UserID = @UserID";
                                    using (SqlCommand updateCmd = new SqlCommand(updateLoginTimeQuery, con))
                                    {
                                        updateCmd.Parameters.AddWithValue("@UserID", userID);
                                        updateCmd.ExecuteNonQuery();
                                    }

                                    // Redirect based on role from the database
                                    if (userDbRole == "Admin")
                                    {
                                        Response.Redirect("AdminHome.aspx", false);
                                    }
                                    else // This will handle "User" role and any other non-admin roles
                                    {
                                        Response.Redirect("UserHome.aspx", false);
                                    }
                                    Context.ApplicationInstance.CompleteRequest();
                                    return;
                                }
                                else
                                {
                                    // Password does not match
                                    ltMessage.Text = "<div class='alert alert-danger'>Invalid username/email or password.</div>";
                                }
                            }
                            else
                            {
                                // Username/Email not found
                                ltMessage.Text = "<div class='alert alert-danger'>Invalid username/email or password.</div>";
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"SQL Error during login: {ex.Message}");
                ltMessage.Text = $"<div class='alert alert-danger'>A database error occurred during login. Please try again.</div>";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"General Error during login: {ex.Message}");
                ltMessage.Text = $"<div class='alert alert-danger'>An unexpected error occurred during login. Please try again.</div>";
            }
        }
    }
}