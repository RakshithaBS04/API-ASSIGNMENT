<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminHome.aspx.cs" Inherits="AutoDrive.AdminHome" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>AutoDrive - Admin Home</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    <style>
        body {
            background-color: #e9ecef; /* Lighter grey for admin area */
            font-family: Arial, sans-serif;
        }
        .navbar {
            background-color: #495057 !important; /* Slightly different dark navbar for admin */
        }
        .navbar-brand, .nav-link {
            color: #ffffff !important;
        }
        .navbar-brand:hover, .nav-link:hover {
            color: #ced4da !important;
        }
        .container {
            margin-top: 50px; /* Adjust for fixed navbar */
            padding: 30px;
            background-color: #ffffff;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.15);
        }
        h2 {
            color: #28a745; /* Green for admin heading */
            margin-bottom: 20px;
        }
        .admin-info p {
            font-size: 1.1em;
            margin-bottom: 10px;
        }
        .admin-actions .list-group-item {
            border: none;
            padding-left: 0;
        }
        .admin-actions .list-group-item a {
            font-size: 1.1em;
            color: #17a2b8; /* Info blue for admin links */
            text-decoration: none;
        }
        .admin-actions .list-group-item a:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-dark fixed-top">
        <div class="container-fluid">
            <a class="navbar-brand" href="AdminHome.aspx">AutoDrive Admin</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav ms-auto">
                    <li class="nav-item">
                        <a class="nav-link" href="Cars.aspx">Manage Cars</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="BookingList.aspx">View All Bookings</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="#">Manage Users</a> <%-- Placeholder --%>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="Login.aspx?logout=true">Logout</a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>

    <div class="container">
        <h2>Welcome, <asp:Literal ID="ltUsername" runat="server"></asp:Literal>!</h2>
        <p>This is the administration dashboard for AutoDrive Car Dealership. You have full control over the system.</p>
        <hr />

        <div class="admin-info mb-4">
            <h4>Your Admin Details:</h4>
            <p><strong>User ID:</strong> <asp:Literal ID="ltUserID" runat="server"></asp:Literal></p>
            <p><strong>Role:</strong> <asp:Literal ID="ltUserRole" runat="server"></asp:Literal></p>
        </div>

        <div class="admin-actions">
            <h4>Admin Actions:</h4>
            <ul class="list-group">
                <li class="list-group-item"><a href="Cars.aspx">Manage Car Listings (Add, Edit, Delete Cars)</a></li>
                <li class="list-group-item"><a href="BookingList.aspx">View and Manage All Customer Bookings</a></li>
                <li class="list-group-item"><a href="#">Manage Website Users</a> <%-- Placeholder for user management page --%>
                </li>
                <%-- Add more admin actions as needed --%>
            </ul>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>