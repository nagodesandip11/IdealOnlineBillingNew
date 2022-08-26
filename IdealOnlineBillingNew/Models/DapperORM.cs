using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
namespace IdealOnlineBillingNew.Models
{
    public class DapperORM
    {
        
      private static  SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
   // private static string connectionString = WebConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public static void ExecuteWithoutReturn(string procedureName,DynamicParameters param=null)
        {   
                con.Open();
                con.Execute(procedureName, param, commandType: CommandType.StoredProcedure);
        }
        public static T ExecuteReturnScalar<T>(string procedureName, DynamicParameters param=null)
        {
            con.Open();
          return (T) Convert.ChangeType(con.Execute(procedureName, param, commandType: CommandType.StoredProcedure),typeof(T));
        }

        public static IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param=null)
        {
            con.Open();
            return con.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
        }

    }
}