using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml.Linq;
using Zero_Hunger.DB;

namespace Zero_Hunger.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize]
        public ActionResult AdminPanel()
        {
            return View();
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
        public ActionResult Login(Admin admin)
        {
            var db = new ZHEntities();
            var us = (from ad in db.Admins
                      where ad.username == admin.username && ad.Password == admin.Password
                      select ad).FirstOrDefault();
            if (us != null)
            {
                string Uname = us.username.ToString();
                FormsAuthentication.SetAuthCookie(Uname, false);
                return RedirectToAction("AdminPanel", "Admin");
            }
            TempData["error"] = "Worng username or password";
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        [Authorize]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(FormCollection form)
        {
            var user = User.Identity.Name;
            string Opass = form["Old_Password"];
            string Npass = form["New_Password"];
            string Cpass = form["Con_Password"];
            var db = new ZHEntities();
            
            if (Opass != null && Npass != null && Cpass != null)
            {
                var us = (from ad in db.Admins
                          where ad.username == user && ad.Password == Opass
                          select ad).FirstOrDefault();
                if (us != null)
                {
                    if (Npass == Cpass)
                    {
                        if (User.Identity.IsAuthenticated)
                        {

                            var n_P = (from pd in db.Admins
                                       where pd.username == user
                                       select pd).FirstOrDefault();
                            n_P.Password = Npass;
                            db.SaveChanges();
                            TempData["error"] = "Successfully Changed password.";
                            return RedirectToAction("AdminPanel", "Admin");

                        }
                        else
                            return RedirectToAction("Login", "Admin");
                    }
                    else
                    {
                        TempData["error"] = "New password and confirm password doesn't match!";
                        return View();
                    }
                }
                else
                {
                    TempData["error"] = "Incorrect password!";
                    return View();
                }
                
            }
            else
            {
                TempData["error"] = "Please fill up information properly!";
            }
            return View();



        }

    }
}