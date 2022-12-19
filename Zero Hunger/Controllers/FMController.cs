using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebSockets;
using Zero_Hunger.DB;
using Zero_Hunger.Models;

namespace Zero_Hunger.Controllers
{

    public class FMController : Controller
    {
        // GET: FM
        [Authorize]
        public ActionResult Index()
        {
            var db = new ZHEntities();
            var donat = db.Donations.ToList();
            return View(donat);
        }
        [Authorize]
        public ActionResult OnlyCollect()
        {
            var db = new ZHEntities();
            var don = (from pd in db.Donations
                       where pd.Status == "Collectable"
                       select pd).ToList();
            return View(don);
        }
        [Authorize]

        public ActionResult Processed()
        {
            var db = new ZHEntities();
            var don = (from pd in db.Donations
                       where pd.Status == "Processed"
                       select pd).ToList();
            return View(don);
        }
        [Authorize]
        public ActionResult OnlyDistribute()
        {
            var db = new ZHEntities();
            var don = (from pd in db.Donations
                       where pd.Status == "Deliverable"
                       select pd).ToList();
            return View(don);
        }
        [Authorize]

        [HttpGet]
        public ActionResult Collect(int id)
        {
            var db = new ZHEntities();
            var don = (from pd in db.Donations
                       where pd.Id == id
                       select pd).ToList();
            var emp = db.Employees.ToList();

            //ViewBag.emplloyee = emp;
            ////TempData["message"] = "Successfully Edited.";
            //return View(don);
            var colList = db.Collections.ToList();

            donation_Class donation_Class = new donation_Class();
            donation_Class.emp = emp;
            donation_Class.don = don;
            donation_Class.col = colList;

            //donation_Class.EmpId


            return View(donation_Class);
        }
        [HttpPost]
        public ActionResult Collect(int DonId, string DonLoc, string DonAdd, String ExpIn, int empId)
        {
            var db = new ZHEntities();
            var emp = (from ad in db.Employees
                      where ad.Id == empId
                      select ad).FirstOrDefault();
            var food = (from ad in db.Donations
                       where ad.Id == DonId
                       select ad).FirstOrDefault();
            Collection colec = new Collection();
            Donation dona = new Donation();

            if (emp != null && food != null)
            {
                colec.EmpId = empId;
                colec.DonationId = DonId;
                colec.Status = "In Process";
                db.Collections.Add(colec);
                db.SaveChanges();

                var status_Change = (from ad in db.Donations
                            where ad.Id == DonId
                            select ad).FirstOrDefault();
                status_Change.Status = "Deliverable";
                db.SaveChanges();
                TempData["error"] = "Successfully Assigned!.";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["error"] = "Something wrong!.";
                return View();
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Distribute(int id)
        {
            var db = new ZHEntities();
            var don = (from pd in db.Donations
                       where pd.Id == id
                       select pd).ToList();
            var emp = db.Employees.ToList();
            //ViewBag.emplloyee = emp;
            ////TempData["message"] = "Successfully Edited.";
            //return View(don);
            var colList = db.Collections.ToList();

            donation_Class donation_Class = new donation_Class();
            donation_Class.emp = emp;
            donation_Class.don = don;
            donation_Class.col = colList;

            //donation_Class.EmpId


            return View(donation_Class);
        }
        [HttpPost]
        public ActionResult Distribute(int DonId, string DonLoc, string DonAdd, String ExpIn, int empId)
        {
            var db = new ZHEntities();
            var emp = (from ad in db.Employees
                       where ad.Id == empId
                       select ad).FirstOrDefault();
            var food = (from ad in db.Donations
                        where ad.Id == DonId
                        select ad).FirstOrDefault();
            Collection colec = new Collection();
            Donation dona = new Donation();

            if (emp != null && food != null)
            {
                colec.EmpId = empId;
                colec.DonationId = DonId;
                colec.Status = "In Process";
                db.Collections.Add(colec);
                db.SaveChanges();

                var status_Change = (from ad in db.Donations
                                     where ad.Id == DonId
                                     select ad).FirstOrDefault();
                status_Change.Status = "Processed";
                db.SaveChanges();
                TempData["error"] = "Successfully Assigned!.";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["error"] = "Something wrong!.";
                return View();
            }
        }
        [Authorize]
        public ActionResult Track()
        {
            var db = new ZHEntities();
            return View(db.Collections.ToList());
        }
    }
}