using LifeSureProject.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeSureProject.Controllers.Admin
{
    public class AdminServiceController : Controller
    {
       
        LifeSureDbEntities1 db = new LifeSureDbEntities1();
        public ActionResult AdminServiceList()
        {
            var value = db.TblService.FirstOrDefault();
            return View(value); ;
        }

        [HttpGet]
        public ActionResult UpdateAdminService(int id)
        {
            var value = db.TblService.Find(id);
            return View(value);

        }

        [HttpPost]
        public ActionResult UpdateAdminService(TblService tblService)
        {
            var value = db.TblService.Find(tblService.ServiceId);
            value.Title = tblService.Title;
            value.Description = tblService.Description;           
                       
            db.SaveChanges();
            return RedirectToAction("AdminServiceList");
        }
    }
}