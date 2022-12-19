using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger.DB;

namespace Zero_Hunger.Controllers
{
    public class LocationController : Controller
    {
        // GET: Location
        [Authorize]
        public ActionResult Index()
        {
            var db = new ZHEntities();
            var emp = db.Locations.ToList();
            return View(emp);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Location l)
        {
            var db = new ZHEntities();
            db.Locations.Add(l);
            db.SaveChanges();
            TempData["error"] = "Successfully Location Created.";
            return RedirectToAction("AdminPanel", "Admin");
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var db = new ZHEntities();
            var loc = (from pd in db.Locations
                       where pd.Id == id
                       select pd).FirstOrDefault();
            //TempData["message"] = "Successfully Edited.";
            return View(loc);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Location loc)
        {
            var db = new ZHEntities();
            var e = (from pd in db.Locations
                     where pd.Id == loc.Id
                     select pd).FirstOrDefault();
            e.LocationName = loc.LocationName;

            db.SaveChanges();
            TempData["error"] = "Successfully Edited.";
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            var db = new ZHEntities();
            var a = db.Locations.Where(i => i.Id == id).SingleOrDefault();
            db.Locations.Remove(a);
            db.SaveChanges();
            TempData["error"] = "Successfully deleted.";
            return RedirectToAction("Index");

        }
        [Authorize]
        public ActionResult Details(int id)
        {
            var db = new ZHEntities();
            var e = (from pd in db.Locations
                     where pd.Id == id
                     select pd).FirstOrDefault();
            return View(e);
        }
    }
}