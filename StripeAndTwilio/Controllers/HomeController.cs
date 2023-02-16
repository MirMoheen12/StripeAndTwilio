using Stripe.Checkout;
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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult CreateCheckoutSession()
        {
            return View();
        }
        [HttpPost]
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
                                Name = "T-Shirt"

                            },

                        },
                        Quantity = 2,
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