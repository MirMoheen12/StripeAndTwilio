using StripeAndTwilio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StripeAndTwilio.Controllers
{
    public class AdminSideController : Controller
    {
        DbEntities db=new DbEntities(); 
        // GET: AdminSide
        public ActionResult Index()
        {
            if (Session["Admin"] != "Active")
            {
                return RedirectToAction("Login","Accountside");
            }
            return View();
        }
        public ActionResult UpdatePackage(int id=0)
        {
            if (Session["Admin"] != "Active")
            {
                return RedirectToAction("Login", "Accountside");
            }
            var data=db.Packages.Where(x=>x.PID==id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult UpdatePackage(Package package)
        {
            if (Session["Admin"] != "Active")
            {
                return RedirectToAction("Login", "Accountside");
            }
            db.Packages.AddOrUpdate(package);
            db.SaveChanges();
            return RedirectToAction("AllPackage", "AdminSide");
        }

        public ActionResult AllPackage()
        {
            if (Session["Admin"] != "Active")
            {
                return RedirectToAction("Login", "Accountside");
            }
            var data=db.Packages.ToList();
            return View(data);
        }
        public ActionResult ActiveOrder()
        {
            if (Session["Admin"] != "Active")
            {
                return RedirectToAction("Login", "Accountside");
            }
            var data = db.getOrderinfo("Active").ToList();
            return View(data);
        }
        public ActionResult AddSupplier()
        {
            if (Session["Admin"] != "Active")
            {
                return RedirectToAction("Login", "Accountside");
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddSupplier(Supplier supplier)
        {
            if (Session["Admin"] != "Active")
            {
                return RedirectToAction("Login", "Accountside");
            }
            db.Suppliers.Add(supplier);
            db.SaveChanges();
            return View();
        }
        public ActionResult AllSupplier()
        {

            if (Session["Admin"] != "Active")
            {
                return RedirectToAction("Login", "Accountside");
            }
            var data = db.Suppliers.ToList();
            return View(data);
        }
        public ActionResult Logout()
        {
            if (Session["Admin"] != "Active")
            {
                return RedirectToAction("Login", "Accountside");
            }
            Session["Admin"] = "NotFound";
            return RedirectToAction("Login", "Accountside");
        }
    }
}