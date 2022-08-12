using CRUDAjax.Models;
using System;
using System.Collections.Generic;
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