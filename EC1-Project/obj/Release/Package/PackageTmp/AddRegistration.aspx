<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddRegistration.aspx.cs" Inherits="EC1_Project.AddRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <center>
        <h2>Link Registration to Profile</h2><hr /><br />
        <asp:Label ID="MessageLabel" runat="server" ForeColor="red"></asp:Label><br />
        <asp:TextBox ID="SearchTextBox" runat="server" class="txtbox" placeholder="Registration Id#"></asp:TextBox><br />
        <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButtonClick" /><br />
        <hr />
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        <asp:Button ID="LinkButton" runat="server" Text="Link" OnClick="LinkButtonClick"/>
    </center>
</asp:Content>
