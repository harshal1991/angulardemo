using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Configuration;


namespace Angularextra
{
    /// <summary>
    /// Summary description for StudentService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class StudentService : System.Web.Services.WebService
    {

        [WebMethod]
        public void GetCityName()
        {
            List<student> liststudent = new List<student>();
            string cs = ConfigurationManager.ConnectionStrings["AccessSoftware"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select * from tableCity", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    student stu = new student();
                    stu.CityId = Convert.ToInt32(rdr["CityId"]);
                    stu.CityName = rdr["CityName"].ToString();
                    stu.CityCode = rdr["CityCode"].ToString();
                    liststudent.Add(stu);
                }


            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(liststudent));
        }

        //------WITH ID------------//
        [WebMethod]
        public void GetCityNameById(int CityId)
        {
            student stud = new student();
            string cs = ConfigurationManager.ConnectionStrings["AccessSoftware"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select * from tableCity where CityId=@CityId", con);

                SqlParameter param = new SqlParameter()
                {
                    ParameterName = "@CityId",
                    Value = CityId

                };
                cmd.Parameters.Add(param);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    stud.CityId = Convert.ToInt32(rdr["CityId"]);
                    stud.CityName = rdr["CityName"].ToString();
                    stud.CityCode = rdr["CityCode"].ToString();

                }


            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(stud));
        }


        //------WITH Name------------//
        [WebMethod]
        public void GetCityNameByName(string CityName)
        {
            
            List<student> liststudent = new List<student>();
            string cs = ConfigurationManager.ConnectionStrings["AccessSoftware"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select * from tableCity where CityName like @CityName", con);

                SqlParameter param = new SqlParameter()
                {
                    ParameterName = "@CityName",
                    Value = CityName + "%"

                };
                cmd.Parameters.Add(param);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    student stud = new student();
                    stud.CityId = Convert.ToInt32(rdr["CityId"]);
                    stud.CityName = rdr["CityName"].ToString();
                    stud.CityCode = rdr["CityCode"].ToString();
                    liststudent.Add(stud);

                }


            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(liststudent));
        }
    }
}

