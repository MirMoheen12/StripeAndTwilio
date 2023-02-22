using Stripe.Checkout;
using StripeAndTwilio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace StripeAndTwilio.Controllers
{
    public class HomeController : Controller
    {
        DbEntities db=new DbEntities();
        public ActionResult Index()
        {
            var fat = db.Customers.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Index(int mint = 0)
        {
            //var fat = db.Customers.ToList();
            return RedirectToAction("pakageSelection", "Home", new { mint = mint });
        }
        [HttpGet]
        public ActionResult pakageSelection(int mint=12)
        {
            int newmint = mint / 60;
            var data = db.Packages.ToList();
            for (int i = 0; i < data.Count; i++)
            {
                data[i].Intialamount = data[i].Intialamount + newmint;
            }
            return View(data);
        }
        public ActionResult About()
        {
            var fat = db.Customers.ToList();
            ViewBag.Message = "Your application description page.";

            return View(fat);
        }
        [HttpGet]
        public ActionResult CreateCheckoutSession(string amount)
        {
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = Convert.ToInt32(amount) * 100,
                            Currency = "inr",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Rent"

                            },

                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = "https://localhost:44381/Home/Index",
                CancelUrl = "https://localhost:44381/Home/About",
            };
            var services = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = services.Create(options);
            Response.Headers.Add("Location", session.Url);
            return new HttpStatusCodeResult(303);
        }
 

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}