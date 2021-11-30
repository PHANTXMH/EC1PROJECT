<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EC1_Project.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <center>
        <asp:Label ID="LoginLabel" runat="server" Text="TAJ Login"></asp:Label><br /><br />
        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Invalid Login, Please retry" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate" ControlToValidate="PasswordTextBox"></asp:CustomValidator><br /><br />
        <asp:Label ID="UsernameLabel" runat="server" Text="Username: "></asp:Label>
        <asp:TextBox ID="UsernameTextBox" runat="server"></asp:TextBox><br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Username is Required" ControlToValidate="UsernameTextBox" ForeColor="Red"></asp:RequiredFieldValidator><br /><br />        
        <asp:Label ID="PasswordLabel" runat="server" Text="Password: "></asp:Label>
        <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password"></asp:TextBox><br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Password is required" ControlToValidate="PasswordTextBox" ForeColor="Red"></asp:RequiredFieldValidator><br /><br />        
        <asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="LoginButton_Click" /><br /><br />
    </center>
    
</asp:Content>
