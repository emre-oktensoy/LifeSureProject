using LifeSureProject.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeSureProject.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        LifeSureDbEntities1 db = new LifeSureDbEntities1();
        public ActionResult ContactPartialDefault()
        {
            var value = db.TblContact.FirstOrDefault();

            return PartialView("_ContactPartialView",value);
        }
    }
}