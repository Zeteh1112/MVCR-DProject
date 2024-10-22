using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MVCR_DProject
{
    public partial class Autocomplete : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //-- actual  webmethod that will fetch data from database based on what user typed   
        [WebMethod]
        public static string[] GetEmployeeData(string SearchParam)
        {
            List<string> empList = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select EmpName, EmpId from EmployeeTable where EmpName like @SearchParam + '%'";
                    cmd.Parameters.AddWithValue("@SearchParam", SearchParam);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                            empList.Add(string.Format("{0}-{1}", sdr["EmpName"], sdr["EmpId"]));
                    }
                    conn.Close();
                }
            }
            return empList.ToArray();
        }

        //-- generate sample data for auto complete just to demonstrate  
        [WebMethod]
        public static string[] GetEmployeeDataSample(string SearchParam)
        {
            List<string> empList = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                string data = (i + 1).ToString();
                data += "-EmpName " + (i + 1).ToString();
                empList.Add(data);
            }
            return empList.Where(x => x.ToLower().Contains(SearchParam)).ToArray();
        }
    }
}