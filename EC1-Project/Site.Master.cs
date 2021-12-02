using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EC1_Project
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Global.user == null)
            {
                UserLabel.Visible = false;
                LoginLabel.Text = "Login";
            }
            else
            {
                UserLabel.Visible = true;
                UserLabel.Text = "Hi "+Global.user.Username+"!";
                LoginLabel.Text = "Log out";
            }
        }
    }
}