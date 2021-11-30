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
            if(Global.user == null)
            {
                Response.Redirect("Login.aspx");
            }
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM vehicle JOIN registration ON vehicle.registrationid = registration.id WHERE registration.id = @id;");
            cmd.Connection = new NpgsqlConnection(Global.dbcon);
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("@id", int.Parse(Session["registrationid"].ToString()));
            NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            

            if (!IsPostBack)
            {
                ExpTextBox.Text = reader.GetValue(9).ToString();
                VMakeTextBox.Text = reader.GetValue(3).ToString();
                VTypeTextBox.Text = reader.GetValue(4).ToString();
                ChassisTextBox.Text = reader.GetValue(2).ToString();
                PlateTextBox.Text = reader.GetValue(1).ToString();
            }
            cmd.Connection.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {            
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = new NpgsqlConnection(Global.dbcon);
            cmd.Connection.Open();

            if (MonthsRadioButton.Checked)
            {                        
                cmd.CommandText = "UPDATE registration SET expdate = @expdate WHERE id = @id;";
                cmd.Parameters.AddWithValue("@expdate", DateTime.Parse(ExpTextBox.Text.Trim()).Date.AddMonths(6));
                cmd.Parameters.AddWithValue("@id", int.Parse(Session["registrationid"].ToString()));
                cmd.ExecuteNonQuery();
                cmd.CommandText = "UPDATE vehicle SET vehiclemake = @vehiclemake, vehicletype = @vehicletype," +
                    " chassisnumber = @chassisnumber, licenseplatenumber = @licenseplatenumber WHERE registrationid = @registrationid;";
                cmd.Parameters.AddWithValue("@vehiclemake", VMakeTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@vehicletype", VTypeTextBox.Text.Trim());
                string check = VTypeTextBox.Text.Trim();
                cmd.Parameters.AddWithValue("@chassisnumber", ChassisTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@licenseplatenumber", PlateTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@registrationid", int.Parse(Session["registrationid"].ToString()));                        

                if(cmd.ExecuteNonQuery() < 1)
                {
                    Response.Write("<script>alert('Could not process');</script>");
                }

            }
            else
            if (YearRadioButton.Checked)
            {
                cmd.CommandText = "UPDATE registration SET expdate = @expdate WHERE id = @id;";
                cmd.Parameters.AddWithValue("@expdate", DateTime.Parse(ExpTextBox.Text.Trim()).Date.AddYears(1));
                cmd.Parameters.AddWithValue("@id", int.Parse(Session["registrationid"].ToString()));
                cmd.ExecuteNonQuery();
                cmd.CommandText = "UPDATE vehicle SET vehiclemake = @vehiclemake, vehicletype = @vehicletype," +
                    " chassisnumber = @chassisnumber, licenseplatenumber = @licenseplatenumber WHERE registrationid = @registrationid;";
                cmd.Parameters.AddWithValue("@vehiclemake", VMakeTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@vehicletype", VTypeTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@chassisnumber", ChassisTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@licenseplatenumber", PlateTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@registrationid", int.Parse(Session["registrationid"].ToString()));

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