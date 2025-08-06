using LifeSureProject.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeSureProject.Controllers.Admin
{
    public class AdminCarouselController : Controller
    {
        
        LifeSureDbEntities1 db = new LifeSureDbEntities1(); 
        public ActionResult AdminCarouselList()
        {
            var value = db.TblCarousel.FirstOrDefault();
            return View(value); ;
        }

        [HttpGet]
        public ActionResult UpdateAdminCarousel(int id)
        {
            var value = db.TblCarousel.Find(id);
            return View(value);

        }

        [HttpPost]
        public ActionResult UpdateAdminCarousel(TblCarousel tblCarousel)
        {
            var value = db.TblCarousel.Find(tblCarousel.CarouselId);
            value.Heading = tblCarousel.Heading;
            value.Title = tblCarousel.Title;
            value.Description = tblCarousel.Description;

            value.Heading_EN = tblCarousel.Heading_EN;
            value.Title_EN = tblCarousel.Title_EN;
            value.Description_EN = tblCarousel.Description_EN;

            db.SaveChanges();
            return RedirectToAction("AdminCarouselList");
        }
    }
}