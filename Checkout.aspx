<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="Project2.Checkout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>Checkout Summary</h3>
            <br />

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Cart Id">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("CartId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="User">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("SUser") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="SubCourse Id">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("SubCourseId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Price">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <br />
            <p>
                Grand total&nbsp;&nbsp;
                <asp:Label ID="Label5" runat="server" Text="0.00"></asp:Label>
            </p>
            <p>
                <asp:Button ID="Button1" runat="server" Text="Pay Now" OnClick="Button1_Click" />
            </p>
        </div>
    </form>
</body>
</html>
