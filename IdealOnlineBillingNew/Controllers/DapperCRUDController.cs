using CRUDAjax.Models;
using IdealOnlineBillingNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdealOnlineBillingNew.Controllers
{
    public class DapperCRUDController : Controller
    {
        // GET: DapperCRUD
        public ActionResult Index()
        {
            return View(DapperORM.ReturnList<Employee>("",null));
        }
    }
}