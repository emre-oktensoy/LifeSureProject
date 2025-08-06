using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeSureProject.Controllers
{
    public class ModalSearchController : Controller
    {
        // GET: ModalSearch
        public ActionResult ModalSearchPartialDefault()
        {
            return PartialView("_ModalSearchPartial");
        }
    }
}