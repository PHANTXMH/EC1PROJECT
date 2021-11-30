<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MVIS.aspx.cs" Inherits="EC1_Project.MVIS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>MVIS</title>
    </head>
    <body>
        <div >                
            <nav>
                <h1>MVIS</h1>
            </nav>                      
        </div>
        <div>
            <center>
                <h3>Enter your vehicle registration number or chassis number to check your vehicle insurance status</h3><hr /><br /><br />      
                    
                <form id="form1" runat="server">
                    <asp:TextBox ID="PlateTextBox" runat="server" placeholder="License Plate #"></asp:TextBox>&nbsp;&nbsp;
                    <asp:TextBox ID="ChassisTextBox" runat="server" placeholder="Enter Chassis #"></asp:TextBox>&nbsp;&nbsp;
                    <asp:Button ID="SearchButton" runat="server" Text="Search" style="background-color:green; color:white" OnClick="SearchButton_Click"/>
                </form><br /><br />               
                    <asp:Label ID="Label1" runat="server" ></asp:Label><br />                 
                   <asp:Image ID="Image1" runat="server" height="120" width="120"/><br /><br />
                

            </center>
        </div>
    </body>
</html>

