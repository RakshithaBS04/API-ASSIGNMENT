using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace AutoDrive
{
    public partial class Signup : System.Web.UI.Page
    {
        private string connectionString = "Data Source=DESKTOP-00GRLI4;Database=web_application;Integrated Security=True;trustservercertificate=true";

        protected void Page_Load(object sender, EventArgs e)
        {
            // No specific logic needed on page load for signup
        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            ltMessage.Text = ""; // Clear previous messages

            if (!Page.IsValid)
            {
                return;
            }

            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text; // Password obtained in plain text
            string confirmPassword = txtConfirmPassword.Text;

            // Basic input validation
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ltMessage.Text = "<div class='alert alert-danger'>Please fill in all required fields.</div>";
                return;
            }

            if (password != confirmPassword)
            {
                ltMessage.Text = "<div class='alert alert-danger'>Passwords do not match.</div>";
                return;
            }

            // --- IMPORTANT: Storing password in plain text as per your request ---
            string plainTextPassword = password;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Check if username or email already exists to prevent duplicates
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username OR Email = @Email";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@Username", username);
                        checkCmd.Parameters.AddWithValue("@Email", email);
                        int existingCount = (int)checkCmd.ExecuteScalar();

                        if (existingCount > 0)
                        {
                            ltMessage.Text = "<div class='alert alert-danger'>Username or Email already exists. Please choose a different one.</div>";
                            return;
                        }
                    }

                    // Insert new user into the database, including RegistrationDate
                    // This assumes you have added the RegistrationDate column to your Users table.
                    string insertQuery = "INSERT INTO Users (Username, PasswordHash, Email, Role, RegistrationDate) VALUES (@Username, @PasswordHash, @Email, @Role, GETDATE())";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@PasswordHash", plainTextPassword); // Storing plain text password
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Role", "User"); // Default role for new signups

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            ltMessage.Text = "<div class='alert alert-success'>Account created successfully! You can now <a href='Login.aspx'>Login</a>.</div>";
                            txtUsername.Text = "";
                            txtEmail.Text = "";
                            txtPassword.Text = "";
                            txtConfirmPassword.Text = "";
                        }
                        else
                        {
                            ltMessage.Text = "<div class='alert alert-danger'>Account creation failed. Please try again.</div>";
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"SQL Error during signup: {ex.Message}");
                // Provide a more generic error for the user, but log details for debugging
                ltMessage.Text = $"<div class='alert alert-danger'>A database error occurred during signup. Please ensure all fields are correct and try again.</div>";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"General Error during signup: {ex.Message}");
                ltMessage.Text = $"<div class='alert alert-danger'>An unexpected error occurred during signup. Please try again.</div>";
            }
        }
    }
}