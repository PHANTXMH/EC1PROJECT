<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateRegistration.aspx.cs" Inherits="EC1_Project.UpdateRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <center>
        <h2>Update Registration</h2><hr />
        <asp:Label ID="NameLabel" runat="server" Text="Name/s on Registration"></asp:Label>
        <asp:DropDownList ID="NameDropDownList" runat="server"></asp:DropDownList><br /><br />
        <asp:Label ID="ExpLabel" runat="server" Text="Expired-At: "></asp:Label>
        <asp:TextBox ID="ExpTextBox" runat="server" Enabled="false"></asp:TextBox><br />
        <asp:RadioButton ID="MonthsRadioButton" runat="server" Text="6 months" Checked="true" GroupName="Expiration"></asp:RadioButton>&nbsp;&nbsp;
        <asp:RadioButton ID="YearRadioButton" runat="server" Text="1 year" GroupName="Expiration"></asp:RadioButton><br /><br />
        <asp:Label ID="VMakeLable" runat="server" Text="Vehicle Make: "></asp:Label>
        <asp:TextBox ID="VMakeTextBox" runat="server" Enabled="false"></asp:TextBox><br /><br /><br />
        <asp:Label ID="VTypeLabel" runat="server" Text="Vehicle Type: "></asp:Label>
        <asp:TextBox ID="VTypeTextBox" runat="server" Enabled="false"></asp:TextBox><br /><br /><br />
        <asp:Label ID="ChassisLabel" runat="server" Text="Chassis #: "></asp:Label>
        <asp:TextBox ID="ChassisTextBox" runat="server" Enabled="false"></asp:TextBox><br /><br /><br />
        <asp:Label ID="PlateLabel" runat="server" Text="License Plate #: "></asp:Label>
        <asp:TextBox ID="PlateTextBox" runat="server" Enabled="false"></asp:TextBox><br /><br /><br />
        <asp:Button ID="Button1" runat="server" Text="Renew" OnClick="Button1_Click" />
    </center> 

</asp:Content>
