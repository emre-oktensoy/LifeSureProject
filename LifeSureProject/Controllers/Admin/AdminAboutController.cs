using LifeSureProject.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeSureProject.Controllers.Admin
{
    public class AdminAboutController : Controller
    {
       LifeSureDbEntities1 db = new LifeSureDbEntities1();  
        // GET: AdminAbout
        public ActionResult AdminAboutList()
        {
            var value = db.TblAbout.FirstOrDefault();
            return View(value);
            
        }
        [HttpGet]
        public ActionResult UpdateAdminAbout(int id)
        {
            var value = db.TblAbout.Find(id);
            return View(value);       

        }

        [HttpPost]  
        public ActionResult UpdateAdminAbout(TblAbout tblAbout)
        {
            var value = db.TblAbout.Find(tblAbout.AboutId);
            value.Title = tblAbout.Title;
            value.Description = tblAbout.Description;           
            db.SaveChanges();
            return RedirectToAction("AdminAboutList");
        }   
    }
}