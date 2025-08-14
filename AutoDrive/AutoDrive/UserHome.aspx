<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserHome.aspx.cs" Inherits="AutoDrive.UserHome" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>AutoDrive - User Home</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    <style>
        body {
            background-color: #f8f9fa;
            font-family: Arial, sans-serif;
        }
        .navbar {
            background-color: #343a40 !important; /* Dark navbar */
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
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }
        h2 {
            color: #007bff;
            margin-bottom: 20px;
        }
        .user-info p {
            font-size: 1.1em;
            margin-bottom: 10px;
        }
        .user-actions .list-group-item {
            border: none;
            padding-left: 0;
        }
        .user-actions .list-group-item a {
            font-size: 1.1em;
            color: #007bff;
            text-decoration: none;
        }
        .user-actions .list-group-item a:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-dark fixed-top">
        <div class="container-fluid">
            <a class="navbar-brand" href="UserHome.aspx">AutoDrive</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav ms-auto">
                    <li class="nav-item">
                        <a class="nav-link" href="AvailableCars.aspx">Available Cars</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="MyBookings.aspx">My Bookings</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="Favorites.aspx">Favorites</a>
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
        <p>This is your personalized dashboard for AutoDrive Car Dealership.</p>
        <hr />

        <div class="user-info mb-4">
            <h4>Your Account Details:</h4>
            <p><strong>User ID:</strong> <asp:Literal ID="ltUserID" runat="server"></asp:Literal></p>
            <p><strong>Role:</strong> <asp:Literal ID="ltUserRole" runat="server"></asp:Literal></p>
        </div>

        <div class="user-actions">
            <h4>Quick Actions:</h4>
            <ul class="list-group">
                <li class="list-group-item"><a href="AvailableCars.aspx">Browse Available Cars</a></li>
                <li class="list-group-item"><a href="MyBookings.aspx">View/Manage Your Bookings</a></li>
                <li class="list-group-item"><a href="Favorites.aspx">View Your Favorite Cars</a></li>
                <%-- Add more user actions as needed --%>
            </ul>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>