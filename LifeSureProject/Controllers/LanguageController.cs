using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeSureProject.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Change(string lang, string returnUrl)
        {
            HttpCookie cookie = new HttpCookie("lang", lang)
            {
                Expires = DateTime.Now.AddYears(1)
            };

            Response.Cookies.Add(cookie);

            return Redirect(returnUrl ?? "/");
        }
    }
}