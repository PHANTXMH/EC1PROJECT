using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EC1_Project
{
    public partial class MVFS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Image1.Visible = false;
            Label1.Visible = false;
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {            
            Fitness.FitnessWebService service = new Fitness.FitnessWebService();

            string chassisnum = ChassisTextBox.Text.Trim();            

            if (service.FitnessUpToDate(chassisnum))
            {
                Label1.Text = "Your vehicle is COVERED";
                Label1.ForeColor = System.Drawing.Color.Green;
                Image1.ImageUrl = "images/foundimage.png";
                Label1.Visible = true;
                Image1.Visible = true;
            }
            else
            {
                Label1.Text = "Your vehicle is NOT COVERED";
                Label1.ForeColor = System.Drawing.Color.Red;
                Image1.ImageUrl = "images/errorimage.jpg";                
                Image1.Visible = true;
                Label1.Visible = true;
            }
        }
    }
}