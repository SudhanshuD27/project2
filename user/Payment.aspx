<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="Project2.user.Payment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment</title>
    <!-- Razorpay Checkout.js -->
    <script src="https://checkout.razorpay.com/v1/checkout.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Confirm Your Subscription</h2>

        <asp:Repeater ID="rptSubscription" runat="server">
            <HeaderTemplate>
                <table border="1">
                    <tr>
                        <th>Plan</th>
                        <th>Price</th>
                        <th>Duration</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("PlanName") %></td>
                    <td>₹<%# Eval("Price") %></td>
                    <td><%# Eval("DurationInDays") %> Days</td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <h3>Total: <asp:Label ID="lblAmount" runat="server" /></h3>
        <asp:Button ID="btnPay" runat="server" Text="Pay Now" OnClick="btnPay_Click" />
    </form>
</body>
</html>
