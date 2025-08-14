<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cars.aspx.cs" Inherits="AutoDrive.Cars" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Cars - AutoDrive</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4 mb-5">
            <h2 class="text-center mb-4">Manage Cars</h2>

            <asp:HiddenField ID="hfCarID" runat="server" />

            <div class="row mb-3">
                <div class="col-md-6">
                    <label>Make</label>
                    <asp:TextBox ID="txtMake" CssClass="form-control" runat="server" />
                </div>
                <div class="col-md-6">
                    <label>Model</label>
                    <asp:TextBox ID="txtModel" CssClass="form-control" runat="server" />
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label>Year</label>
                    <asp:TextBox ID="txtYear" CssClass="form-control" runat="server" TextMode="Number" />
                </div>
                <div class="col-md-6">
                    <label>Color</label>
                    <asp:TextBox ID="txtColor" CssClass="form-control" runat="server" />
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label>Mileage</label>
                    <asp:TextBox ID="txtMileage" CssClass="form-control" runat="server" TextMode="Number" />
                </div>
                <div class="col-md-6">
                    <label>Transmission</label>
                    <asp:TextBox ID="txtTransmission" CssClass="form-control" runat="server" />
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label>Fuel Type</label>
                    <asp:TextBox ID="txtFuel" CssClass="form-control" runat="server" />
                </div>
                <div class="col-md-6">
                    <label>Price</label>
                    <asp:TextBox ID="txtPrice" CssClass="form-control" runat="server" TextMode="Number" />
                </div>
            </div>

            <div class="mb-3">
                <label>Description</label>
                <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="4" />
            </div>

            <div class="mb-3 form-check">
                <asp:CheckBox ID="chkAvailable" runat="server" CssClass="form-check-input" />
                <label class="form-check-label">Is Available</label>
            </div>

            <div class="text-center mb-4">
                <asp:Button ID="btnAdd" runat="server" Text="Add Car" CssClass="btn btn-success me-2" OnClick="btnAdd_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary me-2" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnDelete_Click" />
            </div>

            <asp:GridView ID="gvCars" runat="server" CssClass="table table-bordered" AutoGenerateSelectButton="True"
                OnSelectedIndexChanged="gvCars_SelectedIndexChanged" AutoGenerateColumns="True" />
        </div>
    </form>
</body>
</html>
