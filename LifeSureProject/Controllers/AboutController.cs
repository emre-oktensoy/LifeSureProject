using LifeSureProject.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeSureProject.Controllers
{
    public class AboutController : Controller
    {
        LifeSureDbEntities1 db = new LifeSureDbEntities1();
        public ActionResult AboutPartialDefault()
        {
            var value = db.TblAbout.FirstOrDefault();
            return PartialView("_AboutPartialView",value);      
        }

        public ActionResult AboutBoxPartialDefault()
        {
            var values = db.TblAboutBox.ToList();
            return PartialView("_AboutBoxPartialView", values);
        }
    }
}