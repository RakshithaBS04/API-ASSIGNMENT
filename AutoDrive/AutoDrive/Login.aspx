<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AutoDrive.Login" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>AutoDrive - Login</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    <style>
        body {
            background-color: #f0f2f5;
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            font-family: Arial, sans-serif;
        }
        .login-container {
            background-color: #ffffff;
            padding: 40px;
            border-radius: 8px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
            width: 100%;
            max-width: 400px;
            text-align: center;
        }
        .login-container h2 {
            color: #007bff;
            margin-bottom: 30px;
            font-weight: 600;
        }
        .form-label {
            text-align: left;
            display: block;
            margin-bottom: 5px;
            font-weight: 500;
        }
        .form-control {
            margin-bottom: 15px;
            height: 45px;
        }
        .btn-login {
            width: 100%;
            padding: 12px;
            font-size: 1.1rem;
            background-color: #007bff;
            border-color: #007bff;
            margin-top: 10px;
        }
        .btn-login:hover {
            background-color: #0056b3;
            border-color: #0056b3;
        }
        .register-link, .forgot-password-link {
            display: block;
            margin-top: 20px;
            color: #007bff;
            text-decoration: none;
        }
        .register-link:hover, .forgot-password-link:hover {
            text-decoration: underline;
        }
        .alert {
            margin-top: 20px;
            padding: 10px;
            font-size: 0.9em;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2>Login to AutoDrive</h2>

            <asp:Literal ID="ltMessage" runat="server" EnableViewState="false"></asp:Literal>

            <div class="mb-3 text-start">
                <label for="<%= txtUsername.ClientID %>" class="form-label">Username or Email</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" PlaceHolder="Enter username or email"></asp:TextBox>
            </div>
            <div class="mb-3 text-start">
                <label for="<%= txtPassword.ClientID %>" class="form-label">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" PlaceHolder="Enter your password"></asp:TextBox>
            </div>

            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary btn-login" OnClick="btnLogin_Click" />

            <div class="mt-3">
                Don't have an account? <a href="Signup.aspx" class="register-link">Sign Up Here</a>
            </div>
            <div>
                <a href="#" class="forgot-password-link">Forgot Password?</a> <%-- Placeholder for future implementation --%>
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>