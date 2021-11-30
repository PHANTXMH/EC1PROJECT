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
    public partial class ViewRegistration : System.Web.UI.Page
    {
        int rid;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Global.user == null)
            {
                Response.Redirect("Login.aspx");
            }

            NpgsqlConnection con = new NpgsqlConnection(Global.dbcon);
            NpgsqlCommand cmd;
            NpgsqlDataReader registrationRecordReader, registrationIdReader;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                cmd = new NpgsqlCommand("SELECT registrationid FROM user_registration WHERE userid = @userid;",con);
                cmd.Parameters.AddWithValue("@userid", Global.user.Id);
                registrationIdReader = cmd.ExecuteReader();
                List<int> usersList = new List<int>();
                

                while (registrationIdReader.Read())
                {
                    usersList.Add(int.Parse(registrationIdReader["registrationid"].ToString()));
                }
                registrationIdReader.Close();

                rid = 1;

                foreach(int regid in usersList)
                {                   
                    cmd.CommandText = "SELECT * FROM vehicle JOIN registration ON vehicle.registrationid = registration.id WHERE registration.id = "+regid+";";
                    //cmd.Parameters.AddWithValue("@id", regid);                    
                    registrationRecordReader = cmd.ExecuteReader();
                    
                    if (registrationRecordReader.Read())
                    {
                        Label idLabel = new Label();
                        idLabel.Text = "ID: " + registrationRecordReader.GetValue(8).ToString();
                        PlaceHolder1.Controls.Add(idLabel);
                        PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));                       

                        Label expLabel = new Label();
                        expLabel.Text = "Expired-At: " + registrationRecordReader.GetValue(9).ToString();
                        PlaceHolder1.Controls.Add(expLabel);
                        PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));
                       
                        Label vehiclemake = new Label();
                        vehiclemake.Text = "Vehicle Make: " + registrationRecordReader.GetValue(3).ToString();
                        PlaceHolder1.Controls.Add(vehiclemake);
                        PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));                       

                        Label vehicletype = new Label();
                        vehicletype.Text = "Vehicle Type: " + registrationRecordReader.GetValue(4).ToString();
                        PlaceHolder1.Controls.Add(vehicletype);
                        PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));                        

                        Label chassisnum = new Label();
                        chassisnum.Text = "Chassis #: " + registrationRecordReader.GetValue(2).ToString();
                        PlaceHolder1.Controls.Add(chassisnum);
                        PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));                        

                        Label lpnum = new Label();
                        lpnum.Text = "License Plate #: " + registrationRecordReader.GetValue(1).ToString();
                        PlaceHolder1.Controls.Add(lpnum);
                        PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));

                        Button button = new Button();
                        button.ID = rid.ToString();
                        button.Text = "Update";
                        button.Click += new EventHandler(UpdateButtonClick);
                        PlaceHolder1.Controls.Add(button);
                        PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));
                        PlaceHolder1.Controls.Add(new LiteralControl("<hr />"));
                        rid++;
                    }
                    registrationRecordReader.Close();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }

           
        }

        public void UpdateButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int id = int.Parse(button.ID);
            Insurance.InsuranceWebService insuranceWebService = new Insurance.InsuranceWebService();
            Fitness.FitnessWebService fitnessWebService = new Fitness.FitnessWebService();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT chassisnumber, licenseplatenumber FROM vehicle WHERE registrationid = @id");
            cmd.Connection = new NpgsqlConnection(Global.dbcon);
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                if (insuranceWebService.InsuranceUpToDate(reader["chassisnumber"].ToString(), reader["licenseplatenumber"].ToString()))
                {
                    if (fitnessWebService.FitnessUpToDate(reader["chassisnumber"].ToString()))
                    {                        
                        Session["registrationid"] = id;
                        Response.Redirect("UpdateRegistration.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('The fitness for this vehicle is expired, please come into our offices and renew');</script>");
                    }
                }else
                {
                    Response.Write("<script>alert('The fitness for this vehicle is expired, please come into our offices and renew');</script>");
                }
            }    
            
        }
    }
}