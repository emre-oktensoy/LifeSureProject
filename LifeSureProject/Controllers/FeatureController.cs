using LifeSureProject.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeSureProject.Controllers
{
    public class FeatureController : Controller
    {
        LifeSureDbEntities1 db = new LifeSureDbEntities1();
        public ActionResult FeaturePartialDefault()
        {
            var value = db.TblFeature.FirstOrDefault();
            return PartialView("_FeaturePartialView",value);
        }

        public ActionResult FeatureBoxPartialDefault()
        {
            var values = db.TblFeatureBox.ToList();
            return PartialView("_FeatureBoxPartialView",values);
        }
    }
}