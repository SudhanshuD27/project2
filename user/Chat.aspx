<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="Project2.user.Chat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
        <h2>Chat with Admin</h2>

        <asp:Repeater ID="rptChat" runat="server">
            <ItemTemplate>
                <div>
                    <b><%# Convert.ToBoolean(Eval("IsFromAdmin")) ? "Admin" : "You" %>:</b>
                    <%# Eval("Message") %> <i style="font-size:small;">(<%# Eval("SentAt") %>)</i>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <br />
        <asp:TextBox ID="txtMessage" runat="server" Width="300px" />
        <asp:Button ID="btnSend" runat="server" Text="Send" OnClick="btnSend_Click" />
    </form>
</body>
</html>
