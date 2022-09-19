using IdealOnlineBillingNew.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdealOnlineBillingNew.Controllers
{
    public class PartialAndRenderPartialController : Controller
    {
        IdealWebDB db = new IdealWebDB();
        // GET: PartialAndRenderPartial
        public ActionResult Index()
        {
            var emp = db.Employees.ToList();
            // return PartialView("_EmployeeList", emp);
            return View(emp);
        }
        public ActionResult Edit(int id)
        {

            return View();
        }
    }
}