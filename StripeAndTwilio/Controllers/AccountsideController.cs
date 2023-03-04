using StripeAndTwilio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StripeAndTwilio.Controllers
{
    public class AccountsideController : Controller
    {
        DbEntities db = new DbEntities();
        // GET: Accountside
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AllUser allUser)
        {
            var data = db.AllUsers.Where(x => x.Username == allUser.Username && x.UserPassword == allUser.UserPassword).FirstOrDefault();
            if (data != null)
            {
                Session["Admin"] = "Active";
                return RedirectToAction("Index", "AdminSide");
            }
            return View();
        }
    }
}