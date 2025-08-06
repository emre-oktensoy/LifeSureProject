using LifeSureProject.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeSureProject.Controllers
{
    public class HomeController : Controller
    {

        LifeSureDbEntities1 db = new LifeSureDbEntities1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Layout()
        {
            return View();
        }



    }
}