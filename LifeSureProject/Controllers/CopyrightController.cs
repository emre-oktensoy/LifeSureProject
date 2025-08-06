using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeSureProject.Controllers
{
    public class CopyrightController : Controller
    {
        // GET: Copyright
        public ActionResult CopyrightPartialDefault()
        {
            return PartialView("_CopyrightPartialView", "Copyright");
        }
    }
}