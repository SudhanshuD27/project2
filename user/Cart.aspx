<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="Project2.user.Cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Your Cart</h2>
        <asp:Repeater ID="rptCart" runat="server" OnItemCommand="rptCart_ItemCommand">
            <ItemTemplate>
                <div style="border:1px solid #ccc; padding:10px; margin:10px;">
                    <img src='<%# Eval("Thumbnail") %>' width="100" />
                    <h3><%# Eval("Title") %></h3>
                    <p>Price: ₹<%# Eval("Price") %></p>
                    <asp:Button ID="btnRemove" runat="server" CommandName="Remove" 
                        CommandArgument='<%# Eval("CartId") %>' Text="Remove" />
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <hr />
        <p><strong>Grand Total: ₹</strong><asp:Label ID="lblGrandTotal" runat="server" Text="0.00"></asp:Label></p>
        <asp:Button ID="btnCheckout" runat="server" Text="Proceed to Checkout" OnClick="btnCheckout_Click" />
    </form>
</body>
</html>
