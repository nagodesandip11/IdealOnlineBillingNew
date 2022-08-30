using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web;
using IdealOnlineBillingNew.Context;

namespace CRUDAjax.Models
{
    public class EmployeeDB
    {
        IdealWebDB db = new IdealWebDB();
        //declare connection string
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        //Return list of all Employees
        public List<Employee> ListAll()
        {
            List<Employee> lst = new List<Employee>();
            using(SqlConnection con=new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("SelectEmployee",con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while(rdr.Read())
                {
                    
                    lst.Add(new Employee { 
                        EmployeeID=Convert.ToInt32(rdr["EmployeeId"]),
                        Name=rdr["Name"].ToString(),
                        Age = Convert.ToInt32(rdr["Age"]),
                        State = rdr["State"].ToString(),
                        Country = rdr["Country"].ToString(),
                        ImageBytes =(byte[])rdr["ImageBytes"] ,
                        ImagePath = Convert.ToBase64String((byte[])rdr["ImageBytes"]),
                    });
                }


             }
           
            return lst;
        }

        public List<Employee> StateList()
        {
            List<Employee> lst = new List<Employee>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("SelectEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new Employee
                    {
                        EmployeeID = Convert.ToInt32(rdr["EmployeeId"]),
                        Name = rdr["Name"].ToString(),
                        Age = Convert.ToInt32(rdr["Age"]),
                        State = rdr["State"].ToString(),
                        Country = rdr["Country"].ToString(),
                    });
                }
                return lst;
            }
        }

        //Method for Adding an Employee
        public int Add(Employee emp)
        {
            HttpPostedFile postedFile;
            var fileName = emp.ImageFile;
            string newFileName = Guid.NewGuid() + Path.GetExtension(fileName.FileName);
            string path = System.Web.HttpContext.Current.Server.MapPath("~/Images");
            fileName.SaveAs(path + newFileName);
            System.Web.Hosting.HostingEnvironment.MapPath("~/Images/"+fileName.FileName+"");
            
               
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(fileName.InputStream);
            imageBytes = reader.ReadBytes(fileName.ContentLength);

            int i=0;
            if (emp.Name!=null)
            { 
           
            using(SqlConnection con=new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("InsertUpdateEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id",emp.EmployeeID);
                com.Parameters.AddWithValue("@Name", emp.Name);
                com.Parameters.AddWithValue("@Age", emp.Age);
                com.Parameters.AddWithValue("@State", emp.State);
                com.Parameters.AddWithValue("@Country", emp.Country);
                com.Parameters.AddWithValue("@ImagePath", path);
                com.Parameters.AddWithValue("@ImageBytes", imageBytes);
                com.Parameters.AddWithValue("@ImageName", fileName.FileName);
                
                    com.Parameters.AddWithValue("@Action", "Insert");
                i = com.ExecuteNonQuery();
            }

            }
            return i;
        }

        //Method for Updating Employee record
        public int Update(Employee emp)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("InsertUpdateEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", emp.EmployeeID);
                com.Parameters.AddWithValue("@Name", emp.Name);
                com.Parameters.AddWithValue("@Age", emp.Age);
                com.Parameters.AddWithValue("@State", emp.State);
                com.Parameters.AddWithValue("@Country", emp.Country);
                com.Parameters.AddWithValue("@Action", "Update");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        //Method for Deleting an Employee
        public int Delete(int ID)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("DeleteEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", ID);
                i = com.ExecuteNonQuery();
            }
            return i;
        }
    }
}
