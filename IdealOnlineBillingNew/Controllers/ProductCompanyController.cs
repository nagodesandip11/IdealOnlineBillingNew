using CRUDAjax.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdealOnlineBillingNew.Controllers
{
    public class ProductCompanyController : Controller
    {
        EmployeeDB empDB = new EmployeeDB();
        // GET: ProductCompany
        public ActionResult Index()
        {

            return View();
        }
        public JsonResult List()
        {
            return Json(empDB.ListAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetState()
        {
            return Json(empDB.StateList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(Employee emp)
        {

            //HttpPostedFile postedFile;
            var fileName = emp.ImageFile;
            string newFileName = fileName.FileName;//Guid.NewGuid() + Path.GetExtension(fileName.FileName); 
            string extension = Path.GetExtension(fileName.FileName); 
            fileName.SaveAs(Path.Combine(Server.MapPath("~/Images"), newFileName)); 
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(fileName.InputStream);
            imageBytes = reader.ReadBytes(fileName.ContentLength);
            emp.ImagePath = newFileName;
            emp.ImageBytes = imageBytes;
            return Json(empDB.Add(emp), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Employee emp)
        {
            return Json(empDB.Update(emp), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            return Json(empDB.Delete(ID), JsonRequestBehavior.AllowGet);
        }
    }
}