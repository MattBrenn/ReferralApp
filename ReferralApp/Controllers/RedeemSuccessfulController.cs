using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReferralApp.Models;
using ReferralApp.ServiceReference1;
using RestSharp;

namespace ReferralApp.Controllers
{
    public class RedeemSuccessfulController : Controller
    {
        // GET: RedeemSuccessful
        public ActionResult Index(RedeemSucces model)
        {
            try
            {
                //ViewBag.RedeemSuccess = "Your coupon code worth €" + model.Value + " is:";
                //ViewBag.CouponCode = model.Code;

                return View("RedeemSuccessful");

            }
            catch (Exception ex)
            {
                Logger.Error("Error redeemsuccess model not passed in", ex);
                throw;
            }
           
        }
    }
}