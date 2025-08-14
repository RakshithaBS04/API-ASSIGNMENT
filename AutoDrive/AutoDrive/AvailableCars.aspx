<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AvailableCars.aspx.cs" Inherits="AutoDrive.AvailableCars" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>AutoDrive - Available Cars</title>
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
        .car-card {
            border: 1px solid #e0e0e0;
            border-radius: 8px;
            padding: 15px;
            margin-bottom: 20px;
            background-color: #fdfdfd;
            box-shadow: 0 2px 5px rgba(0,0,0,0.05);
            transition: transform 0.2s ease-in-out;
            height: 100%; /* Ensures all cards in a row have equal height */
            display: flex;
            flex-direction: column;
        }
        .car-card:hover {
            transform: translateY(-5px);
        }
        /*.car-card img {
            max-width: 100%;
            height: 200px;*/ /* Fixed height for images */
            /*object-fit: cover;*/ /* Ensures image covers area without distortion */
            /*border-radius: 5px;
            margin-bottom: 15px;
        }*/
        .car-card h5 {
            color: #333;
            margin-bottom: 10px;
            font-size: 1.25rem;
        }
        .car-card p {
            font-size: 0.95em;
            margin-bottom: 5px;
            color: #555;
        }
        .car-card .price {
            font-size: 1.3em;
            font-weight: bold;
            color: #28a745;
            margin-top: 10px;
            margin-bottom: 15px;
        }
        .car-card .card-description {
            font-size: 0.85em;
            color: #666;
            margin-bottom: 15px;
            flex-grow: 1; /* Allows description to take available space */
            max-height: 80px; /* Limit description height */
            overflow: hidden;
            text-overflow: ellipsis;
        }
        .btn-book {
            width: 100%;
            margin-top: auto; /* Pushes button to the bottom */
        }
        .alert {
            margin-top: 20px;
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
                            <a class="nav-link active" aria-current="page" href="AvailableCars.aspx">Available Cars</a>
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
            <h2>Our Available Vehicles</h2>

            <asp:Literal ID="ltMessage" runat="server" EnableViewState="false"></asp:Literal>

            <div class="row">
                <asp:Repeater ID="rptCars" runat="server" OnItemCommand="rptCars_ItemCommand">
                    <ItemTemplate>
                        <div class="col-lg-4 col-md-6 col-sm-12 d-flex">
                            <div class="car-card">
                                <h5><%# Eval("Year") %> <%# Eval("Make") %> <%# Eval("Model") %></h5>
                                <p><strong>Color:</strong> <%# Eval("Color") %></p>
                                <p><strong>Mileage:</strong> <%# Convert.ToInt32(Eval("Mileage")).ToString("N0", System.Globalization.CultureInfo.InvariantCulture) %> km</p>
                                <p><strong>Transmission:</strong> <%# Eval("Transmission") %></p>
                                <p><strong>Fuel Type:</strong> <%# Eval("FuelType") %></p>
                                <div class="card-description"><%# Eval("Description") %></div>
                                <div class="price"><%# Convert.ToDecimal(Eval("Price")).ToString("C", System.Globalization.CultureInfo.InvariantCulture) %></div>
                                <asp:LinkButton ID="btnBook" runat="server" CommandName="BookCar" CommandArgument='<%# Eval("CarID") %>'
                                    CssClass="btn btn-primary btn-book">Book Now</asp:LinkButton>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    </form>
</body>
</html>
