<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Subscription.aspx.cs" Inherits="Project2.user.Subscription" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style>
        body { font-family: Arial; padding: 20px; }
        table { width: 100%; border-collapse: collapse; }
        th, td { padding: 8px; border: 1px solid #ccc; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Available Subscription Plans</h2>

        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
        <br /><br />

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="PlanId"
            OnRowCommand="GridView1_RowCommand" GridLines="Both">
            <Columns>
                <asp:BoundField DataField="PlanName" HeaderText="Plan Name" />
                <asp:BoundField DataField="Price" HeaderText="Price" />
                <asp:BoundField DataField="DurationInDays" HeaderText="Duration (Days)" />
                <asp:BoundField DataField="CreatedAt" HeaderText="Created On" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:ButtonField ButtonType="Button" CommandName="ViewCourses" Text="View Courses" />
                <asp:ButtonField ButtonType="Button" CommandName="Subscribe" Text="Subscribe" />
            </Columns>
        </asp:GridView>

        <br />

        <asp:Panel ID="Panel1" runat="server" Visible="false">
            <h3>Courses Included in this Plan:</h3>
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="True" GridLines="Both" />
        </asp:Panel>
    </form>
</body>
</html>
