<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddRegistration.aspx.cs" Inherits="EC1_Project.AddRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <center>
        <h2>Link Registration to Profile</h2><hr /><br />
        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="This registration is already linked." ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate" ControlToValidate="SearchTextBox"></asp:CustomValidator><br />
        <asp:Label ID="Label1" runat="server" Text="Enter Registration Id: "></asp:Label>&nbsp;&nbsp;
        <asp:TextBox ID="SearchTextBox" runat="server"></asp:TextBox>&nbsp;&nbsp;
        <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="Button1_Click" />&nbsp;&nbsp;<br /><br />
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
    </center>
</asp:Content>
