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
    /// Summary description for InsuranceWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    public class InsuranceWebService : System.Web.Services.WebService
    {
        public InsuranceWebService()
        {

        }       

        [WebMethod]
        public bool InsuranceUpToDate(string chassisnum, string licenseplatenum)
        {
            NpgsqlConnection con = new NpgsqlConnection(Global.dbcon);
            NpgsqlCommand cmd = new NpgsqlCommand();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                
                cmd = new NpgsqlCommand("SELECT insuranceid FROM vehicle WHERE chassisnumber = @chassisnum OR licenseplatenumber = @lpnum;", con);
                cmd.Parameters.AddWithValue("@chassisnum", chassisnum);
                cmd.Parameters.AddWithValue("@lpnum", licenseplatenum);                
                NpgsqlDataReader insuranceidReader = cmd.ExecuteReader();

                if (insuranceidReader.Read())
                {
                    string value = insuranceidReader["insuranceid"].ToString();
                    insuranceidReader.Close();

                    cmd.CommandText = "SELECT expdate FROM insurance WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", int.Parse(value));
                    NpgsqlDataReader expdateReader = cmd.ExecuteReader();
                    expdateReader.Read();

                    string insuranceexpdate = expdateReader["expdate"].ToString();
                    int days = Int32.Parse((DateTime.Parse(insuranceexpdate).Date - DateTime.Now.Date).TotalDays.ToString());
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
                //Response.Write("<script>alert('" + ex.Message + "');</script>");
                con.Close();
                return false;
            }
            
        }       
    }
}
