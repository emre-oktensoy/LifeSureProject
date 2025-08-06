using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeSureProject.Controllers
{
    public class FooterController : Controller
    {
        // GET: Footer
        public ActionResult FooterPartialDefault()
        {
            return PartialView("_FooterPartialView","Footer");
        }
    }
}