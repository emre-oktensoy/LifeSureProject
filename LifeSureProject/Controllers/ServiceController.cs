using LifeSureProject.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeSureProject.Controllers
{
    public class ServiceController : Controller
    {
        LifeSureDbEntities1 db = new LifeSureDbEntities1();
        // GET: Service
        public ActionResult ServicePartialDefault()
        {
            //var value = db.TblService.FirstOrDefault();
            try
            {
                var value = db.TblService.FirstOrDefault();
                return PartialView("_ServicePartialView", value);
            }
            catch (Exception ex)
            {
                return Content("Hata oluştu: " + ex.Message); 
            }
           
        }

        public ActionResult ServiceBoxPartialDefault()
        {
            var values   = db.TblServiceBox.ToList();
            return PartialView("_ServiceBoxPartialView", values);
        }
    }
}