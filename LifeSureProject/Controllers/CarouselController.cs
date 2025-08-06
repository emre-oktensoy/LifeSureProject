using LifeSureProject.Models.DataModels;
using LifeSureProject.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace LifeSureProject.Controllers
{
    public class CarouselController : Controller
    {
        LifeSureDbEntities1 db = new LifeSureDbEntities1(); 

        public ActionResult CarouselPartialDefault()
        {

            string lang = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

            var value = db.TblCarousel.Select(s => new CarouselViewModel
            {
                Heading = lang == "en" ? s.Heading_EN : s.Heading,
                Title = lang == "en" ? s.Title_EN : s.Title,
                Description = lang == "en" ? s.Description_EN : s.Description
            }).FirstOrDefault();

           

            return PartialView("_CarouselPartialView",value);
        }
    }
}