using LifeSureProject.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeSureProject.Controllers
{
    public class FAQController : Controller
    {
        LifeSureDbEntities1 db = new LifeSureDbEntities1(); 
        public ActionResult FAQPartialDefault()
        {
            var values = db.TblFAQ.ToList();
            return PartialView("_FAQPartialView",values);
        }
    }
}