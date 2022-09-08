<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EC1_Project.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <center>
        
        <div class="row">
            <div class="col">
                
                <img padding="5px" src="images/user.png" />
                <br />                
                <asp:Label CssClass="thick" ID="LoginLabel" runat="server" Text="TAJ Member Login"></asp:Label><br />
                <br />
                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Invalid Login, Please retry" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate" ControlToValidate="PasswordTextBox"></asp:CustomValidator><br />
                <br />                
                <asp:TextBox class="txtbox" ID="UsernameTextBox" runat="server" Height="30px" Width="196px" placeholder="Username"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Username is Required" ControlToValidate="UsernameTextBox" ForeColor="Red"></asp:RequiredFieldValidator>
                <br />
                <br />                
                <asp:TextBox class="txtbox" ID="PasswordTextBox" runat="server" TextMode="Password" Height="30px" Width="187px" placeholder="Password"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Password is required" ControlToValidate="PasswordTextBox" ForeColor="Red"></asp:RequiredFieldValidator>                            
                <br />
                <br />                
                <asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="LoginButton_Click" BorderColor="DodgerBlue" Width="66px" /><br />
                <br />
    </center>
</asp:Content>

