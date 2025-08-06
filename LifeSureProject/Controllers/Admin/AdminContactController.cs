using LifeSureProject.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeSureProject.Controllers.Admin
{
    public class AdminContactController : Controller
    {
        
        LifeSureDbEntities1 db = new LifeSureDbEntities1();
     
        public ActionResult AdminContactList()
        {
            var value = db.TblContact.FirstOrDefault();
            return View(value);

        }
        [HttpGet]
        public ActionResult UpdateAdminContact(int id)
        {
            var value = db.TblContact.Find(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult UpdateAdminContact(TblContact tblContact)
        {
            var value = db.TblContact.Find(tblContact.ContactId);
            value.Address = tblContact.Address;
            value.Mail = tblContact.Mail;
            value.Telephone = tblContact.Telephone;
            db.SaveChanges();
            return RedirectToAction("AdminContactList");
        }
    }
}