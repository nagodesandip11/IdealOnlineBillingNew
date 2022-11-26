using IdealOnlineBillingNew.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace IdealOnlineBillingNew.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
       // IdealWebDB db = new IdealWebDB();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(tblUserLogin model)
        {
            using (var context=new IdealWebDB())
            {
                bool isValid = context.tblUserLogins.Any(x => x.userName == model.userName && x.password == model.password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.userName, false);
                    return RedirectToAction("Index","Home");
                }
                ModelState.AddModelError("", "Invalid Username & password ..!!");
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}