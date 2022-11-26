using IdealOnlineBillingNew.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdealOnlineBillingNew.Controllers
{
    public class ProductTypeController : Controller
    {
        IdealWebDB db = new IdealWebDB();
        // GET: ProductType
        public ActionResult Index()
        {
            List();
            return View();
        }
        public JsonResult List()
        {
            var abc= db.tblCategoryMasters.ToList();
            abc = abc.OrderBy(x => x.categoryName).ToList();
            return Json(abc, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(tblCategoryMaster obj)
        {
            db.tblCategoryMasters.Add(obj);
            int rs=db.SaveChanges();
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(tblCategoryMaster model)
        {
            var datainDB = db.tblCategoryMasters.FirstOrDefault(x => x.categoryId == model.categoryId);
            datainDB.categoryName = model.categoryName;
            
            int result= db.SaveChanges();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            //var res = db.tblCategoryMasters.First(x=>x.categoryId==ID);
            var res = db.tblCategoryMasters.First(x => x.categoryId == ID);
            db.tblCategoryMasters.Remove(res);
             int s=db.SaveChanges();
            return Json(s, JsonRequestBehavior.AllowGet);
        }
    }
}