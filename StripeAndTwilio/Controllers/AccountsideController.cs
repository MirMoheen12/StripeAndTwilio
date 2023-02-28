using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StripeAndTwilio.Controllers
{
    public class AccountsideController : Controller
    {
        // GET: Accountside
        public ActionResult Login()
        {
            return View();
        }
    }
}