using IdealOnlineBillingNew.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdealOnlineBillingNew.Controllers
{
    public class FirmController : Controller
    {
        IdealWebDB db = new IdealWebDB();
        // GET: Firm
        public ActionResult Index()
        {
            List();
            return View();
        }
        public JsonResult List()
        {
            var result = db.tblFirmDetails.OrderBy(s => s.firmName);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Add(tblFirmDetail model)
        {
            var fileName = model.ImageFile;
            string newFileName = fileName.FileName;//Guid.NewGuid() + Path.GetExtension(fileName.FileName); 
            string extension = Path.GetExtension(fileName.FileName);
            fileName.SaveAs(Path.Combine(Server.MapPath("~/Images"), newFileName));
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(fileName.InputStream);
            imageBytes = reader.ReadBytes(fileName.ContentLength);
            model.logoPath = newFileName;
            model.logoBinary = imageBytes;
             db.tblFirmDetails.Add(model);
            int rs = db.SaveChanges();
            return Json(db.tblFirmDetails.ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update(tblFirmDetail model)
        {

            var fileName = model.ImageFile;
            string newFileName = fileName.FileName;//Guid.NewGuid() + Path.GetExtension(fileName.FileName); 
            string extension = Path.GetExtension(fileName.FileName);
            fileName.SaveAs(Path.Combine(Server.MapPath("~/Images"), newFileName));
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(fileName.InputStream);
            imageBytes = reader.ReadBytes(fileName.ContentLength);
            model.logoPath = newFileName;
            model.logoBinary = imageBytes;



            var datainDB = db.tblFirmDetails.FirstOrDefault(x => x.firmId == model.firmId);
            datainDB.firmName = model.firmName;
            datainDB.firmAddress = model.firmAddress;
            datainDB.firmcontact = model.firmcontact;
            datainDB.gstNo = model.gstNo;
            datainDB.subjectTo = model.subjectTo;
            datainDB.firmFor = model.firmFor;
            datainDB.Services = model.Services;
            datainDB.bankName = model.bankName;
            datainDB.acNo = model.acNo;
            datainDB.branchname = model.branchname;
            datainDB.ifscCode = model.ifscCode;
            datainDB.logoPath = model.logoPath;
            datainDB.logoBinary = model.logoBinary;

            db.Entry(datainDB).State = EntityState.Modified;

            int result = db.SaveChanges();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int ID)
        {
            //var res = db.tblCategoryMasters.First(x=>x.categoryId==ID);
            var res = db.tblFirmDetails.First(x => x.firmId == ID);
            db.tblFirmDetails.Remove(res);
            int s = db.SaveChanges(); 
            return Json(s, JsonRequestBehavior.AllowGet);
        }
    }
}