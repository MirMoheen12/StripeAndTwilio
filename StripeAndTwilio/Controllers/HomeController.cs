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
        BookingModel model=new BookingModel();
        public ActionResult Successfullview()
        {
            
            return View();
        }
        public ActionResult Paymentfailed()
        {
            return View();
        }
        public ActionResult Index()
        {
            var fat = db.Customers.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Index(BookingModel booking)
        {
            //var fat = db.Customers.ToList();
            Session["Bookmodel"]=booking;
            return RedirectToAction("pakageSelection", "Home");
        }
        [HttpGet]
        public ActionResult pakageSelection()
        {
            var data2 = (BookingModel)Session["Bookmodel"];
            int newmint = Convert.ToInt32(200) / 60;
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
        public ActionResult CreateCheckoutSession(string amount,int pkg)
        {
            BookingModel md=(BookingModel)Session["Bookmodel"];
            md.Pkgid=pkg;
            Session["Bookmodel"] = md;
            ViewBag.Amount = amount;
            return View();
        }
        [HttpPost]
        public ActionResult CreateCheckoutSession(Customer customer)
        {
        
            db.Customers.Add(customer);
            db.SaveChanges();
            BookingModel md = (BookingModel)Session["Bookmodel"];
            Orderinfo orderinfo=new Orderinfo();
            orderinfo.ONumber = 1231;
            orderinfo.DropOffloc = md.Toloc;
            orderinfo.Pickupplace = md.Toloc;
            orderinfo.Pickuptime = md.bookingdate;
            orderinfo.NetAmount =Convert.ToInt16(customer.Amount);
            db.Orderinfoes.Add(orderinfo);
            db.SaveChanges();
            ConfirmOrder confirmOrder = new ConfirmOrder();
            confirmOrder.Cid= customer.Cid;
            confirmOrder.instruction = "hellow";
            confirmOrder.Notes = "hwllo1";
            confirmOrder.Pid = md.Pkgid;
            confirmOrder.OStatus = "Active";
            db.ConfirmOrders.Add(confirmOrder);
            db.SaveChanges();
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = Convert.ToInt32(customer.Amount) * 100,
                            Currency = "GBP",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Rent"

                            },

                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = "https://airporttaxibook.azurewebsites.net/Home/Successfullview",

                CancelUrl = "https://airporttaxibook.azurewebsites.net/Home/Paymentfailed",
            };
            var services = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = services.Create(options);
            Response.Headers.Add("Location", session.Url);
            return new HttpStatusCodeResult(303);
        }
        public ActionResult PrivacyPloicy()
        {
            return View();
        }


        public ActionResult Termscondition()
        {
            return View();
        }
        public ActionResult cancellationpolicy()
        {
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public void AddMap(BookingModel booking)
        {
            
            model.Luggage = booking.Luggage;
            model.Fromloc = booking.Fromloc;
            model.Toloc = booking.Toloc;
            model.Totalminutes= booking.Totalminutes;
            model.bookingdate= booking.bookingdate;
            model.Passenger=booking.Passenger;

        }
    }
}