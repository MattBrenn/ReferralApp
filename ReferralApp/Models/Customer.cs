using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReferralApp.ServiceReference1;

namespace ReferralApp.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string EmailAdddress { get; set; }

        public string PhoneNumber { get; set; }

        public string AddressLine1 { get; set; }

        public string Password { get; set; }

        public string AddressLine2 { get; set; }

        public string CityTown { get; set; }

        public string County { get; set; }

        public string Country { get; set; }

        public string PostCode { get; set; }

        public DateTime DateCreated { get; set; }

        public CustomerStatus Status { get; set; }

        //public IList<Coupon> Coupons { get; set; }

        public IList<CouponUsage> CouponUsages { get; set; }

        public string ReferralCode { get; set; }
    }
}

