<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="Project2.user.UserProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <style>
        body { font-family: Arial; padding: 20px; }
        label { display: block; margin-top: 10px; }
        input[type="text"], input[type="email"], input[type="password"] {
            width: 300px; padding: 8px; margin-top: 5px;
        }
        .btn { margin-top: 15px; padding: 8px 20px; }
        .success { color: green; margin-top: 15px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>My Profile</h2>

        <label>Full Name</label>
        <asp:TextBox ID="txtName" runat="server" />

        <label>Email</label>
        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" />

        <label>Password (Leave blank to keep unchanged)</label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />

        <br />
        <asp:Button ID="btnUpdate" runat="server" Text="Update Profile" CssClass="btn" OnClick="btnUpdate_Click" />

        <asp:Label ID="lblMessage" runat="server" CssClass="success"></asp:Label>
    </form>
</body>
</html>
