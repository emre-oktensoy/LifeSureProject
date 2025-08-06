using LifeSureProject.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeSureProject.Controllers.Admin
{
    public class AdminFeatureBoxController : Controller
    {
        LifeSureDbEntities1 db = new LifeSureDbEntities1();

        public ActionResult AdminFeatureBoxList()
        {
            var values = db.TblFeatureBox.OrderBy(x => x.FeatureBoxID).Take(4).ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult UpdateAdminFeatureBox(int id)
        {
            var value = db.TblFeatureBox.Find(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult UpdateAdminFeatureBox(TblFeatureBox tblFeatureBox)
        {
            var value = db.TblFeatureBox.Find(tblFeatureBox.FeatureBoxID);
            value.Title = tblFeatureBox.Title;
            value.Description =tblFeatureBox.Description;
            value.ImageUrl =tblFeatureBox.ImageUrl;

            db.SaveChanges();
            return RedirectToAction("AdminFeatureBoxList");
        }
    }
}