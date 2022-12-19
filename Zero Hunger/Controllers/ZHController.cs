using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Zero_Hunger.DB;

namespace Zero_Hunger.Controllers
{
    public class ZHController : Controller
    {
        // GET: ZH
        public ActionResult Home()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Donar don)
        {
            var db = new ZHEntities();
            db.Donars.Add(don);
            db.SaveChanges();
            TempData["error"] = "Successfully Created.";
            return RedirectToAction("CollectionRequest", "Donate");
        }
        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("AdminPanel", "Admin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Donar don)
        {
            var db = new ZHEntities();
            var us = (from ad in db.Donars
                      where ad.username == don.username && ad.Password == don.Password
                      select ad).FirstOrDefault();
            if (us != null)
            {
                string Uname = us.username.ToString();
                FormsAuthentication.SetAuthCookie(Uname, false);
                return RedirectToAction("CollectionRequest", "Donate");
            }
            TempData["error"] = "Worng username or password";
            return View();
        }


    }
}