using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ReferralApp.Models;
using RestSharp;

namespace ReferralApp.Controllers
{
    public class ForgotPasswordController : Controller
    {
        // GET: ForgotPassword
        public ActionResult Index()
        {
            return View("ForgotPassword");
        }

        
        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            
                var apisetting = ConfigurationManager.AppSettings["DynamicApilink"];
                var restClient = new RestClient(apisetting);

                var restRequestLink = string.Format("ForgotPassword?emailAddress={0}", email);
                RestRequest request = new RestRequest(restRequestLink, Method.POST);
                request.AddHeader("Content-type", "application/json; charset=utf-8");

                var apisetting2 = ConfigurationManager.AppSettings["DynamicForgotpassword"];

                var response = restClient.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return GetSuccessView();
                }
                var error = JsonConvert.DeserializeObject<RestError>(response.Content);

            return GetErrorView(error.Message);
        }



        private ActionResult GetErrorView(string errorMessage)
        {
            ViewBag.exceptionthrownbool = true;

            ViewBag.ValidationSummaryMessage = errorMessage;


            return View("ForgotPassword");
        }


        private ActionResult GetSuccessView()
        {
            ViewBag.successbool = true;

            ViewBag.SuccessSummaryMessage = "We've sent you an email to recover your password.";

            

            return View("ForgotPassword");
        }

    }
}