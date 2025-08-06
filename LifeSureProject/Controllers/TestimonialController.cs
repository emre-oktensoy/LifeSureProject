using LifeSureProject.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Markup;

namespace LifeSureProject.Controllers
{
    public class TestimonialController : Controller
    {
        // GET: Testimonial
        LifeSureDbEntities1 db = new LifeSureDbEntities1(); 
        public ActionResult TestimonialPartialDefault()
        {
            var value= db.TblTestimonial.FirstOrDefault();
            return PartialView("_TestimonialPartialView",value);
        }

        public ActionResult TestimonialBoxPartialDefault()
        {
            var values = db.TblTestimonialBox.ToList();
            return PartialView("_TestimonialBoxPartialView", values);
        }
    }
}