using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Data;

namespace EC1_Project
{
    public partial class AddRegistration : System.Web.UI.Page
    {
        int searchid;
        int idfound;
        bool notlinked = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            idfound = 0;
            if (Global.user == null)
            {
                Response.Redirect("Login.aspx");
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                searchid = int.Parse(SearchTextBox.Text.Trim());
            }
            catch (Exception)
            {
                searchid = 0;
            }            

            NpgsqlConnection con = new NpgsqlConnection(Global.dbcon);
            NpgsqlCommand cmd;
            NpgsqlDataReader reader;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                cmd = new NpgsqlCommand("SELECT * FROM vehicle JOIN registration ON vehicle.registrationid = registration.id WHERE registration.id = @id;", con);
                cmd.Parameters.AddWithValue("@id", searchid);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    
                    idfound = int.Parse(reader.GetValue(8).ToString());
                    Label idLabel = new Label();
                    idLabel.Text = "ID: " + idfound.ToString();
                    PlaceHolder1.Controls.Add(idLabel);
                    PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));

                    Label expLabel = new Label();
                    expLabel.Text = "Expired-At: " + reader.GetValue(9).ToString();
                    PlaceHolder1.Controls.Add(expLabel);
                    PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));

                    Label vehiclemake = new Label();
                    vehiclemake.Text = "Vehicle Make: " + reader.GetValue(3).ToString();
                    PlaceHolder1.Controls.Add(vehiclemake);
                    PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));

                    Label vehicletype = new Label();
                    vehicletype.Text = "Vehicle Type: " + reader.GetValue(4).ToString();
                    PlaceHolder1.Controls.Add(vehicletype);
                    PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));

                    Label chassisnum = new Label();
                    chassisnum.Text = "Chassis #: " + reader.GetValue(2).ToString();
                    PlaceHolder1.Controls.Add(chassisnum);
                    PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));

                    Label lpnum = new Label();
                    lpnum.Text = "License Plate #: " + reader.GetValue(1).ToString();
                    PlaceHolder1.Controls.Add(lpnum);
                    PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));

                    Button button = new Button();
                    button.Text = "Link";
                    button.Click += new EventHandler(LinkButtonClick);
                    PlaceHolder1.Controls.Add(button);
                    PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));                   
                                      
                }                
                con.Close();                
            }
            catch (Exception)
            {
                con.Close();
            }
        }
        
        public void LinkButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
            /*NpgsqlConnection con = new NpgsqlConnection(Global.dbcon);
            NpgsqlCommand cmd;            
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                cmd = new NpgsqlCommand("SELECT id from user_registration WHERE userid = @userid AND registrationid = @registrationid;", con);
                cmd.Parameters.AddWithValue("@userid", Global.user.Id);
                cmd.Parameters.AddWithValue("@registrationid",idfound);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())                
                {                    
                    cmd.CommandText = "INSERT INTO user_registration (userid,registrationid) VALUES (@userid, @registrationid);";
                    cmd.Parameters.AddWithValue("@userid", Global.user.Id);
                    cmd.Parameters.AddWithValue("@registrationid", idfound);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("ViewRegistration.aspx");
                }
                else
                {
                    notlinked = false;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }*/
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = notlinked;
        }
    }
}