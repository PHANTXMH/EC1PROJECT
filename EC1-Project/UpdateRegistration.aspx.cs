using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EC1_Project
{
    public partial class UpdateRegistration : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {            
            //Ensures a user is logged in before loading the page
            if(Global.user == null)
            {
                Response.Redirect("Login.aspx");
            }

            //Retrieves all the the registration details based on the registration id given
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT users.fname, users.lname, registration.expdate, vehicle.licenseplatenumber," +
                " vehicle.chassisnumber, vehicle.vehiclemake, vehicle.vehicletype FROM user_registration JOIN users ON" +
                " users.id = user_registration.userid JOIN registration ON registration.id = user_registration.registrationid JOIN vehicle ON" +
                " vehicle.registrationid = user_registration.registrationid WHERE user_registration.registrationid = @id;");
            cmd.Connection = new NpgsqlConnection(Global.dbcon);
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("@id", int.Parse(Session["registrationid"].ToString())); 
            NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();            

            //Only add text to the label object if it is the initial load of the page
            if (!IsPostBack)
            {
                NameDropDownList.Items.Add(reader["fname"].ToString() + " " + reader["lname"].ToString());                
                ExpTextBox.Text = reader.GetValue(2).ToString();
                VMakeTextBox.Text = reader.GetValue(5).ToString();
                VTypeTextBox.Text = reader.GetValue(6).ToString();
                ChassisTextBox.Text = reader.GetValue(4).ToString();
                PlateTextBox.Text = reader.GetValue(3).ToString();
                while (reader.Read())
                {
                    NameDropDownList.Items.Add(reader["fname"].ToString() + " " + reader["lname"].ToString());
                }
            }
            cmd.Connection.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {            
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = new NpgsqlConnection(Global.dbcon);
            cmd.Connection.Open();

            //Adds 6 months to the current expiration date of the registration
            if (MonthsRadioButton.Checked)
            {                        
                cmd.CommandText = "UPDATE registration SET expdate = @expdate WHERE id = @id;";
                cmd.Parameters.AddWithValue("@expdate", DateTime.Parse(ExpTextBox.Text.Trim()).Date.AddMonths(6));
                cmd.Parameters.AddWithValue("@id", int.Parse(Session["registrationid"].ToString()));
                cmd.ExecuteNonQuery();                                   

                if(cmd.ExecuteNonQuery() < 1)
                {
                    Response.Write("<script>alert('Could not process');</script>");
                }
            }
            else
            //Adds a year to the current expiration date of the registration
            if (YearRadioButton.Checked)
            {
                cmd.CommandText = "UPDATE registration SET expdate = @expdate WHERE id = @id;";
                cmd.Parameters.AddWithValue("@expdate", DateTime.Parse(ExpTextBox.Text.Trim()).Date.AddYears(1));
                cmd.Parameters.AddWithValue("@id", int.Parse(Session["registrationid"].ToString()));
                cmd.ExecuteNonQuery();                

                if (cmd.ExecuteNonQuery() < 1)
                {
                    Response.Write("<script>alert('Could not process');</script>");
                }
            }
            cmd.Connection.Close();
            Response.Redirect("ViewRegistration.aspx");
                     
        }
    }
}