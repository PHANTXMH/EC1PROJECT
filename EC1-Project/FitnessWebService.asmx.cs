using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace EC1_Project
{
    /// <summary>
    /// Summary description for FitnessWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    public class FitnessWebService : System.Web.Services.WebService
    {
        public FitnessWebService()
        {

        }

        [WebMethod]
        public bool FitnessUpToDate(string chassisnum)
        {
            NpgsqlConnection con = new NpgsqlConnection(Global.dbcon);
            NpgsqlCommand cmd = new NpgsqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                cmd = new NpgsqlCommand("SELECT fitnessid FROM vehicle WHERE chassisnumber = @chassisnum;", con);
                cmd.Parameters.AddWithValue("@chassisnum", chassisnum);
                NpgsqlDataReader insuranceidReader = cmd.ExecuteReader();

                if (insuranceidReader.Read())
                {
                    string value = insuranceidReader["fitnessid"].ToString();
                    insuranceidReader.Close();

                    cmd.CommandText = "SELECT expdate FROM fitness WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", int.Parse(value));
                    NpgsqlDataReader expdateReader = cmd.ExecuteReader();
                    expdateReader.Read();

                    string fitnessexpdate = expdateReader["expdate"].ToString();
                    int days = Int32.Parse((DateTime.Parse(fitnessexpdate).Date - DateTime.Now.Date).TotalDays.ToString());
                    con.Close();
                    if (days > 0)
                        return true;
                    else
                        return false;
                }
                return false;
                
            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }
        }
    }
}
