using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using NUnit.Framework;
using ReferralApp.Extension;
using ReferralApp.Models;
using ReferralApp.ServiceReference1;
using RestSharp;

namespace ReferralApp.Controllers
{
    public class RedeemController : Controller
    {
        // GET: Redeem
        public ActionResult Index(int customerId)
        {
            try
            {
                AdminClient client = new AdminClient();
                var details = client.GetCustomerSummary(customerId, "76afb1fd-143c-411a-8c8a-4771636a5420");
                var cname = details.Name;
                var firstnameonly = cname.TrimAndReduce();
                ViewBag.RedeemWelcome = "Hi " + firstnameonly + "!";

                var apisetting = ConfigurationManager.AppSettings["DynamicApilink"];
                var restClient = new RestClient(apisetting);

                RestRequest request = new RestRequest("RedeemCoupon?CustId=" + customerId, Method.GET);

                var result = restClient.Execute<decimal>(request);

                ViewBag.RedeemCouponValue = "Congratulations! You have made €" + result.Data + " in free credit. That was easy!";

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return View("Redeem");
                }
                var error = JsonConvert.DeserializeObject<RestError>(result.Content);
                throw new Exception();
            }
            catch (Exception ex)
            {
                Logger.Error("Error coupon details not found", ex);
                throw;
            }


          
        }


        [HttpPost]
        public ActionResult RedeemExecute(int customerid)
        {
            try
            {
                var apisetting = ConfigurationManager.AppSettings["DynamicApilink"];
                var restClient = new RestClient(apisetting);

                var restRequestLink = string.Format("RedeemCoupon?CustId={0}", customerid);
                RestRequest request = new RestRequest(restRequestLink, Method.POST);
                request.AddHeader("Content-type", "application/json; charset=utf-8");


                var response = restClient.Execute<Coupon>(request);


                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return View("~/Views/RedeemSuccessful/RedeemSuccessful.cshtml", new RedeemSucces { Code = response.Data.Code, Value = response.Data.Value });
                }
                var error = JsonConvert.DeserializeObject<RestError>(response.Content);
                throw new Exception();

            }
            catch (Exception ex)
            {
                Logger.Error("Error coupon not found", ex);
                throw;
            }
            
        }
    }
}