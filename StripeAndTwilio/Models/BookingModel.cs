using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StripeAndTwilio.Models
{
    public class BookingModel
    {
        public string Fromloc { get; set; }
        public string Toloc { get; set; }
        public string Totalminutes { get; set; }
        public int Passenger { get; set; }
        public int Luggage { get; set; }
        public int Pkgid { get; set; }
        public string FinalAmount { get; set; }
        public DateTime bookingdate { get; set; }
    }
}