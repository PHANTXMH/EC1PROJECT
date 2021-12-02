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
        bool notlinked = true;
        protected void Page_Load(object sender, EventArgs e)
        {            
            //Ensures a user is logged in before loading the page
            if (Global.user == null)
            {
                Response.Redirect("Login.aspx");
            }

            MessageLabel.Visible = false;                          
            LinkButton.Visible = false;
                      
        }

        protected void SearchButtonClick(object sender, EventArgs e)
        {
            LinkButton.Visible = false;
            MessageLabel.Visible = false;

            //ensures the variable searchid is of type integer
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

                cmd = new NpgsqlCommand("SELECT registration.id, users.fname, users.lname, registration.expdate, vehicle.licenseplatenumber," +
                " vehicle.chassisnumber, vehicle.vehiclemake, vehicle.vehicletype FROM user_registration JOIN users ON" +
                " users.id = user_registration.userid JOIN registration ON registration.id = user_registration.registrationid JOIN vehicle ON" +
                " vehicle.registrationid = user_registration.registrationid WHERE registration.id = @id;",con);
                cmd.Parameters.AddWithValue("@id", searchid);
                reader = cmd.ExecuteReader();                

                if (reader.Read())
                {
                    Label idLabel = new Label();
                    Label nameLabel = new Label();
                    DropDownList dropDownList = new DropDownList();
                    Label expLabel = new Label();
                    Label vehiclemake = new Label();
                    Label vehicletype = new Label();
                    Label chassisnum = new Label();
                    Label lpnum = new Label();

                    Session["idfound"] = reader.GetValue(0).ToString();                    
                    
                    idLabel.Text = "ID: " + Session["idfound"].ToString();
                    

                    
                    nameLabel.Text = "Name/s on Registration: ";


                    dropDownList.Items.Add(reader["fname"].ToString() + " " + reader["lname"].ToString());




                    expLabel.Text = "Expired-At: " + reader.GetValue(3).ToString();
                    

                    
                    vehiclemake.Text = "Vehicle Make: " + reader.GetValue(6).ToString();
                    

                    
                    vehicletype.Text = "Vehicle Type: " + reader.GetValue(7).ToString();
                    

                    
                    chassisnum.Text = "Chassis #: " + reader.GetValue(5).ToString();
                    

                    
                    lpnum.Text = "License Plate #: " + reader.GetValue(4).ToString();

                    while (reader.Read())
                    {
                        dropDownList.Items.Add(reader["fname"].ToString() + " " + reader["lname"].ToString());
                    }


                    //Populate the page with the registration details
                    PlaceHolder1.Controls.Add(idLabel);
                    PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));
                    PlaceHolder1.Controls.Add(nameLabel);
                    PlaceHolder1.Controls.Add(dropDownList);
                    PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));
                    PlaceHolder1.Controls.Add(expLabel);
                    PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));
                    PlaceHolder1.Controls.Add(vehiclemake);
                    PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));
                    PlaceHolder1.Controls.Add(vehicletype);
                    PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));
                    PlaceHolder1.Controls.Add(chassisnum);
                    PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));
                    PlaceHolder1.Controls.Add(lpnum);
                    PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));

                    LinkButton.Visible = true;

                }
                else
                {
                    MessageLabel.Text = "Registration not found";
                    MessageLabel.Visible = true;
                }               
                con.Close();                
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>'");
                con.Close();
            }
        }
        
        public void LinkButtonClick(object sender, EventArgs e)
        {
            LinkButton.Visible = false;
            MessageLabel.Visible = false;
            NpgsqlConnection con = new NpgsqlConnection(Global.dbcon);
            NpgsqlCommand cmd;            
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
               
                cmd = new NpgsqlCommand("SELECT id from user_registration WHERE userid = @userid AND registrationid = @registrationid;", con);
                cmd.Parameters.AddWithValue("@userid", Global.user.Id);
                cmd.Parameters.AddWithValue("@registrationid", int.Parse(Session["idfound"].ToString()));
                NpgsqlDataReader reader = cmd.ExecuteReader();                

                if (!reader.Read())                
                {
                    reader.Close();
                    cmd.CommandText = "INSERT INTO user_registration (userid,registrationid) VALUES (@userid, @registrationid);";
                    cmd.Parameters.AddWithValue("@userid", Global.user.Id);
                    cmd.Parameters.AddWithValue("@registrationid", int.Parse(Session["idfound"].ToString()));
                    cmd.ExecuteNonQuery();
                    reader.Close();
                    Response.Redirect("ViewRegistration.aspx");
                }
                else
                {
                    MessageLabel.Text = "Registration is already linked";
                    MessageLabel.Visible = true;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"');</script>'");
                con.Close();
            }
        }

        
    }
}