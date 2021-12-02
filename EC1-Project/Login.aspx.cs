using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EC1_Project
{
    public partial class Login : System.Web.UI.Page
    {
        bool passwordvalid = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Global.user != null)
            {
                Global.user = null;
                Response.Redirect("Login.aspx");
            }
        }

        
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection(Global.dbcon);
            NpgsqlCommand cmd = new NpgsqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                cmd = new NpgsqlCommand("SELECT * FROM users WHERE username = @username AND password = @password;", con);
                cmd.Parameters.AddWithValue("@username",UsernameTextBox.Text.Trim().ToLower());
                cmd.Parameters.AddWithValue("@password", PasswordTextBox.Text.Trim());
                NpgsqlDataReader reader = cmd.ExecuteReader();

                passwordvalid = reader.Read();
                if (passwordvalid)
                {
                    Global.user = new Users(int.Parse(reader["id"].ToString()), reader["fname"].ToString(), reader["lname"].ToString(), reader["username"].ToString());
                    Response.Redirect("Default.aspx");
                }
            }
            catch (Exception ex)
            {
                con.Close();                
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = passwordvalid;
        }
    }
}