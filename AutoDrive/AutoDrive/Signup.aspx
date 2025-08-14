<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="AutoDrive.Signup" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>AutoDrive - Sign Up</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    <style>
        body {
            background-color: #f8f9fa;
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
        }
        .signup-container {
            background-color: #ffffff;
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            width: 100%;
            max-width: 450px;
        }
        .signup-container h2 {
            margin-bottom: 25px;
            text-align: center;
            color: #343a40;
        }
        .form-label {
            font-weight: 500;
        }
        .btn-primary {
            background-color: #007bff;
            border-color: #007bff;
            width: 100%;
        }
        .btn-primary:hover {
            background-color: #0056b3;
            border-color: #0056b3;
        }
        .text-center a {
            color: #007bff;
            text-decoration: none;
        }
        .text-center a:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="signupForm" runat="server">
        <div class="signup-container">
            <h2>Sign Up for AutoDrive</h2>
            <div class="mb-3">
                <label for="txtUsername" class="form-label">Username</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Required="true" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername"
                    ErrorMessage="Username is required." ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            <div class="mb-3">
                <label for="txtEmail" class="form-label">Email address</label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" Required="true" MaxLength="100"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Email is required." ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ErrorMessage="Invalid Email Format" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
            </div>
            <div class="mb-3">
                <label for="txtPassword" class="form-label">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Required="true" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                    ErrorMessage="Password is required." ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword"
                    ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"
                    ErrorMessage="Min 8 chars, 1 uppercase, 1 lowercase, 1 digit, 1 special char." ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
            </div>
            <div class="mb-3">
                <label for="txtConfirmPassword" class="form-label">Confirm Password</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" Required="true"></asp:TextBox>
                <asp:CompareValidator ID="cvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                    ControlToCompare="txtPassword" ErrorMessage="Passwords do not match." ForeColor="Red" Display="Dynamic"></asp:CompareValidator>
            </div>
            <asp:Literal ID="ltMessage" runat="server" EnableViewState="false"></asp:Literal>
            <asp:Button ID="btnSignup" runat="server" Text="Sign Up" CssClass="btn btn-primary mt-3" OnClick="btnSignup_Click" />
            <p class="text-center mt-3">Already have an account? <a href="Login.aspx">Login here</a></p>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>