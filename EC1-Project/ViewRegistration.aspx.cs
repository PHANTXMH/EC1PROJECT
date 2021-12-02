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
            //Checks if a user is logged in before loading the page
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

                //Searches database for all registrations associated with the logged in user
                cmd = new NpgsqlCommand("SELECT registrationid FROM user_registration WHERE userid = @userid;",con);
                cmd.Parameters.AddWithValue("@userid", Global.user.Id);
                registrationIdReader = cmd.ExecuteReader();
                List<int> usersList = new List<int>();
                

                while (registrationIdReader.Read())
                {
                    usersList.Add(int.Parse(registrationIdReader["registrationid"].ToString()));
                }
                registrationIdReader.Close();                

                foreach(int regid in usersList)
                {        
                    //Retrieves all registration details based on the registration id 
                    cmd.CommandText = "SELECT registration.id, users.fname, users.lname, registration.expdate, vehicle.licenseplatenumber," +
                " vehicle.chassisnumber, vehicle.vehiclemake, vehicle.vehicletype FROM user_registration JOIN users ON" +
                " users.id = user_registration.userid JOIN registration ON registration.id = user_registration.registrationid JOIN vehicle ON" +
                " vehicle.registrationid = user_registration.registrationid WHERE registration.id = "+regid+";";
                    //cmd.Parameters.AddWithValue("@id", regid);                    
                    registrationRecordReader = cmd.ExecuteReader();
                    
                    if(registrationRecordReader.Read())
                    {
                        Label idLabel = new Label();
                        rid = int.Parse(registrationRecordReader.GetValue(0).ToString());
                        idLabel.Text = "ID: " + rid.ToString();
                        

                        Label nameLabel = new Label();
                        nameLabel.Text = "Name/s on Registration: ";
                        

                        DropDownList dropDownList = new DropDownList();                        
                        dropDownList.Items.Add(registrationRecordReader["fname"].ToString() + " " + registrationRecordReader["lname"].ToString());
                        

                        Label expLabel = new Label();
                        expLabel.Text = "Expired-At: " + registrationRecordReader.GetValue(3).ToString();
                       
                       
                        Label vehiclemake = new Label();
                        vehiclemake.Text = "Vehicle Make: " + registrationRecordReader.GetValue(6).ToString();
                                              

                        Label vehicletype = new Label();
                        vehicletype.Text = "Vehicle Type: " + registrationRecordReader.GetValue(7).ToString();
                                              

                        Label chassisnum = new Label();
                        chassisnum.Text = "Chassis #: " + registrationRecordReader.GetValue(5).ToString();
                                                

                        Label lpnum = new Label();
                        lpnum.Text = "License Plate #: " + registrationRecordReader.GetValue(4).ToString();
                        

                        Button button = new Button();
                        button.ID = rid.ToString();
                        button.Text = "Renew";                        
                        button.Click += new EventHandler(UpdateButtonClick);
                        
                        //Adds all the names of users linked to registration to a drop down list
                        while (registrationRecordReader.Read())
                        {
                            dropDownList.Items.Add(registrationRecordReader["fname"].ToString() + " " + registrationRecordReader["lname"].ToString());
                        }

                        //Poplutates the page with registration details
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
                        PlaceHolder1.Controls.Add(button);
                        PlaceHolder1.Controls.Add(new LiteralControl("</br><br/>"));
                        PlaceHolder1.Controls.Add(new LiteralControl("<hr />"));                        
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

            //Checks if the registration's insurance and fitness are up to date before allowing user to update the registration
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