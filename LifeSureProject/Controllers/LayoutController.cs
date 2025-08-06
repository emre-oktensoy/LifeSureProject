using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeSureProject.Controllers
{
    public class LayoutController : Controller
    {
      
        public ActionResult HeadPartialDefault()
        {
            return PartialView("_HeadPartial");
        }

        public ActionResult ScriptPartialDefault()
        {
            return PartialView("_ScriptPartial");
        }
    }
}