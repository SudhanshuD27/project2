<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Invoice.aspx.cs" Inherits="Project2.user.Invoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style>
        body { font-family: Arial; padding: 20px; }
        table { width: 100%; border-collapse: collapse; }
        th, td { padding: 10px; border: 1px solid #ccc; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Subscription Invoice</h2>
        <p><b>Payment ID:</b> <asp:Label ID="lblPaymentId" runat="server" /></p>
        <p><b>Plan:</b> <asp:Label ID="lblPlanName" runat="server" /></p>
        <p><b>Purchase Date:</b> <asp:Label ID="lblPurchaseDate" runat="server" /></p>
        <p><b>Expiry Date:</b> <asp:Label ID="lblExpiryDate" runat="server" /></p>

        <h3>Courses Included:</h3>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" GridLines="Both" />

        <br />
        <asp:Button ID="btnDownloadPdf" runat="server" Text="Download Invoice PDF" OnClick="btnDownloadPdf_Click" />
    </form>
</body>
</html>
