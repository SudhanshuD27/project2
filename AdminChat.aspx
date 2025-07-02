<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminChat.aspx.cs" Inherits="Project2.AdminChat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Chat with User</h2>

        <asp:Label ID="lblUserName" runat="server" Font-Bold="true"></asp:Label>
        <hr />

        <asp:Repeater ID="rptChat" runat="server">
            <ItemTemplate>
                <div style="margin: 5px;">
                    <%# Convert.ToBoolean(Eval("IsFromAdmin")) ? "Admin" : "User" %>:
                    <strong><%# Eval("Message") %></strong>
                    <br />
                    <small><%# Eval("SentAt", "{0:g}") %></small>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <hr />
        <asp:TextBox ID="txtMessage" runat="server" Width="300px" />
        <asp:Button ID="btnSend" runat="server" Text="Send" OnClick="btnSend_Click" />
    </form>
</body>
</html>
