using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StripeAndTwilio.Controllers
{
    public class AdminSideController : Controller
    {
        // GET: AdminSide
        public ActionResult Index()
        {
            return View();
        }
    }
}