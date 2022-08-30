using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CRUDAjax.Models
{
    public class Employee
    {

        //this model used for dapper 
        public int EmployeeID { get; set; }

        public string Name { get; set; }
        
        public int Age { get; set; }
        
        public string State { get; set; }
        
        public string Country { get; set; }
        public string ImagePath { get; set; }
       public Byte[] ImageBytes { get; set; }
        public HttpPostedFileWrapper ImageFile { get; set; }
    }
}
