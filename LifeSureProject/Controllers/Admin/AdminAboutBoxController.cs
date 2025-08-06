using LifeSureProject.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeSureProject.Controllers.Admin
{
    public class AdminAboutBoxController : Controller
    {
        
        LifeSureDbEntities1 db = new LifeSureDbEntities1();
      
        public ActionResult AdminAboutBoxList()
        {
            var values = db.TblAboutBox.OrderBy(x => x.AboutBoxID).Take(4).ToList();
            return View(values);

        }
        [HttpGet]
        public ActionResult UpdateAdminAboutBox(int id)
        {
            var value = db.TblAboutBox.Find(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult UpdateAdminAboutBox(TblAboutBox tblAboutBox)
        {
            var value = db.TblAboutBox.Find(tblAboutBox.AboutBoxID);
            value.Title = tblAboutBox.Title;
            value.Count = tblAboutBox.Count;
            db.SaveChanges();
            return RedirectToAction("AdminAboutBoxList");
        }
    }
}