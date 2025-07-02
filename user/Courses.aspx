<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="Project2.user.Courses" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Available Courses</h2>
        <asp:Repeater ID="rptCourses" runat="server">
            <ItemTemplate>
                <div style="border:1px solid #ccc; padding:10px; margin:10px;">
                    <img src='<%# Eval("Thumbnail") %>' width="100" />
                    <h3><%# Eval("Title") %></h3>
                    <p>Price: ₹<%# Eval("Price") %></p>
                    <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart"
                        CommandArgument='<%# Eval("SubCourseId") %>' OnCommand="btnAddToCart_Command" />
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <br />
        <asp:HyperLink ID="lnkCart" runat="server" NavigateUrl="Cart.aspx">Go to Cart</asp:HyperLink>
    </form>
</body>
</html>
