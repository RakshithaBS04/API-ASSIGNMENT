<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VehicleBooking.aspx.cs" Inherits="AutoDrive.VehicleBooking" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>AutoDrive - Book Vehicle</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    <style>
        body {
            background-color: #f0f2f5;
            font-family: Arial, sans-serif;
        }
        .navbar {
            background-color: #343a40 !important;
        }
        .navbar-brand, .nav-link {
            color: #ffffff !important;
        }
        .navbar-brand:hover, .nav-link:hover {
            color: #ced4da !important;
        }
        .container {
            margin-top: 50px;
            padding: 30px;
            background-color: #ffffff;
            border-radius: 8px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
        }
        h2 {
            color: #007bff;
            margin-bottom: 25px;
            text-align: center;
        }
        .car-details-section {
            border: 1px solid #e0e0e0;
            border-radius: 8px;
            padding: 20px;
            margin-bottom: 30px;
            background-color: #fdfdfd;
        }
        .car-details-section img {
            max-width: 100%;
            height: auto;
            border-radius: 5px;
            margin-bottom: 15px;
        }
        .car-details-section h4 {
            color: #333;
            margin-bottom: 15px;
        }
        .car-details-section p {
            font-size: 1.1em;
            margin-bottom: 8px;
        }
        .price-display {
            font-size: 1.5em;
            font-weight: bold;
            color: #28a745;
            margin-top: 15px;
            border-top: 1px dashed #ddd;
            padding-top: 15px;
        }
        .booking-form-section {
            border: 1px solid #e0e0e0;
            border-radius: 8px;
            padding: 20px;
            background-color: #fdfdfd;
        }
        .form-label {
            font-weight: 500;
        }
        .btn-primary {
            background-color: #007bff;
            border-color: #007bff;
        }
        .btn-primary:hover {
            background-color: #0056b3;
            border-color: #0056b3;
        }
        .validation-message {
            color: red;
            font-size: 0.85em;
        }
        .description-text {
            max-height: 100px; 
            overflow-y: auto;
            border: 1px solid #eee;
            padding: 8px;
            background-color: #f9f9f9;
            border-radius: 4px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
            <h2>Book Your Ride</h2>

            <asp:Literal ID="ltMessage" runat="server" EnableViewState="false"></asp:Literal>

            <div class="row">
                <div class="col-md-6">
                    <div class="car-details-section">
                        <h4>Car Details:</h4>
                       
                        <p><strong>Make:</strong> <asp:Literal ID="ltMake" runat="server"></asp:Literal></p>
                        <p><strong>Model:</strong> <asp:Literal ID="ltModel" runat="server"></asp:Literal></p>
                        <p><strong>Year:</strong> <asp:Literal ID="ltYear" runat="server"></asp:Literal></p>
                        <p><strong>Color:</strong> <asp:Literal ID="ltColor" runat="server"></asp:Literal></p>
                        <p><strong>Mileage:</strong> <asp:Literal ID="ltMileage" runat="server"></asp:Literal> km</p>
                        <p><strong>Transmission:</strong> <asp:Literal ID="ltTransmission" runat="server"></asp:Literal></p>
                        <p><strong>Fuel Type:</strong> <asp:Literal ID="ltFuelType" runat="server"></asp:Literal></p>
                        <p><strong>Price (Original):</strong> <asp:Literal ID="ltOriginalPrice" runat="server"></asp:Literal></p>
                        <p><strong>Daily Rate:</strong> <asp:Literal ID="ltDailyRate" runat="server"></asp:Literal></p>
                        <p class="mt-3"><strong>Description:</strong></p>
                        <div class="description-text">
                            <asp:Literal ID="ltDescription" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="booking-form-section">
                        <h4>Booking Details:</h4>
                        <div class="mb-3">
                            <label for="txtPickupDate" class="form-label">Pickup Date</label>
                            <asp:TextBox ID="txtPickupDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPickupDate" runat="server" ControlToValidate="txtPickupDate" ErrorMessage="Pickup date is required." CssClass="validation-message" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvPickupDate" runat="server" ControlToValidate="txtPickupDate" Type="Date" Operator="GreaterThanEqual" ErrorMessage="Pickup date cannot be in the past." CssClass="validation-message" Display="Dynamic"></asp:CompareValidator>
                        </div>
                        <div class="mb-3">
                            <label for="txtReturnDate" class="form-label">Return Date</label>
                            <asp:TextBox ID="txtReturnDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvReturnDate" runat="server" ControlToValidate="txtReturnDate" ErrorMessage="Return date is required." CssClass="validation-message" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvReturnDate" runat="server" ControlToValidate="txtReturnDate" Type="Date" Operator="GreaterThan" ControlToCompare="txtPickupDate" ErrorMessage="Return date must be after pickup date." CssClass="validation-message" Display="Dynamic"></asp:CompareValidator>
                        </div>

                        <div class="price-display">
                            Estimated Total Cost: <asp:Literal ID="ltTotalCost" runat="server" Text=" "></asp:Literal>
                        </div>

                        <asp:HiddenField ID="hfCarID" runat="server" />
                        <asp:HiddenField ID="hfDailyRate" runat="server" />

                        <div class="d-grid mt-4">
                            <asp:Button ID="btnBookCar" runat="server" Text="Confirm Booking" CssClass="btn btn-primary btn-lg" OnClick="btnBookCar_Click" />
                            <asp:Button ID="btnBack" runat="server" Text="Back to Cars" CssClass="btn btn-secondary btn-lg mt-2" OnClick="btnBack_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    </form>
</body>
</html>