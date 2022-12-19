using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger.DB;

namespace Zero_Hunger.Controllers
{
    public class DonateController : Controller
    {
        [Authorize]
        // GET: Donate
        [HttpGet]
        public ActionResult CollectionRequest()
        {
            //TempData["message"] = "Successfully Edited.";


            return View();
        }
        
        [HttpPost]
        public ActionResult CollectionRequest(Donation donation)
        {
            var db = new ZHEntities();
            donation.Status = "Collectable";
            db.Donations.Add(donation);
            db.SaveChanges();
            TempData["error"] = "Successfully Donated!";
            return RedirectToAction("Home", "ZH");
        }
        public ActionResult Index()
        {
            var db = new ZHEntities();
            var donat = db.Donations.ToList();
            return View(donat);
        }
    }
}