using LifeSureProject.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeSureProject.Controllers
{
    public class TeamController : Controller
    {
        LifeSureDbEntities1 db = new LifeSureDbEntities1();
        public ActionResult TeamPartialDefault()
        {
            var value = db.TblTeam.FirstOrDefault();
            return PartialView("_TeamPartialView", value);
        }

        public ActionResult TeamBoxPartialDefault()
        {
            var values = db.TblTeamBox.ToList();
            return PartialView("_TeamBoxPartialView", values);
        }
    }
}