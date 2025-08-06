using LifeSureProject.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeSureProject.Controllers.Admin
{
    public class AdminFeatureController : Controller
    {

        LifeSureDbEntities1 db = new LifeSureDbEntities1();

        public ActionResult AdminFeatureList()
        {
            var value = db.TblFeature.FirstOrDefault();
            return View(value);

        }
        [HttpGet]
        public ActionResult UpdateAdminFeature(int id)
        {
            var value = db.TblFeature.Find(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult UpdateAdminFeature(TblFeature tblFeature)
        {
            var value = db.TblFeature.Find(tblFeature.FeatureId);
            value.Title = tblFeature.Title;
            value.Description = tblFeature.Description;            
            db.SaveChanges();
            return RedirectToAction("AdminFeatureList");
        }
    }
}