using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ReferralApp.Models;
using ReferralApp.ServiceReference1;
using ReferralCodeGenerator;
using NUnit.Framework;
using RestSharp;
using ReferralApp.Extension;
using Customer = ReferralApp.Models.Customer;

namespace ReferralApp.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        [HttpGet]
        public ActionResult Index(string referralCode)
        {
            try
            {
                CodeGenerator referral = new CodeGenerator();
                var result = referral.GetCustomerIdFromCode(referralCode);

                AdminClient client = new AdminClient();
                var details = client.GetCustomerSummary(result, "76afb1fd-143c-411a-8c8a-4771636a5420");
                var cname = details.Name;

                var firstnameonly = cname.TrimAndReduce();

                @ViewBag.CustomerName = "Join " + firstnameonly + " on Foto Store";

                return View("Register");
            }
            catch (Exception ex)
            {
                Logger.Error("Error getting client details", ex);
                throw;
            }
        }



        [HttpPost]
        public ActionResult RegisterExecute(Client model)
        {
            try
            {
                var apisetting = ConfigurationManager.AppSettings["DynamicApilink"];
                var restClient = new RestClient(apisetting);

                RestRequest request = new RestRequest("customer", Method.POST);
                request.AddHeader("Content-type", "application/json; charset=utf-8");

                var customer = new Models.Customer
                {
                    Name = model.Fname + " " + model.Lname,
                    EmailAdddress = model.Email,
                    Password = model.Password
                };

                var json = JsonConvert.SerializeObject(customer);
                request.AddParameter("text/json", json, ParameterType.RequestBody);

                var response = restClient.Execute<Customer>(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {                 
                        var referralCode = Request.QueryString["referralCode"];
                        CodeGenerator referral = new CodeGenerator();
                        var result = referral.GetCustomerIdFromCode(referralCode);

                        int existingCustId = result;
                        int newCustId = response.Data.CustomerId;

                        var setting = ConfigurationManager.AppSettings["DynamicApilink"];

                        var restClient2 = new RestClient(setting);
                        var restRequestLink = string.Format("customerreferral?ExistCustId={0}&NewCustId={1}",
                            existingCustId, newCustId);
                        RestRequest request1 = new RestRequest(restRequestLink, Method.POST);
                        request1.AddHeader("Content-type", "application/json; charset=utf-8");
                        var result2 = restClient2.Execute(request1);

                        return View("~/Views/DownloadApp/DownloadApp.cshtml");
                }

                var error = JsonConvert.DeserializeObject<RestError>(response.Content);

                return GetErrorView(model, error.Message);
            }
            catch (Exception ex)
            {
                Logger.Error("Error registering new customer", ex);

                return GetErrorView(model, "Oops! Something went wrong. Please try again later.");
            }
        }

        private ActionResult GetErrorView(Client model, string errorMessage)
        {
            ViewBag.exceptionthrownbool = true;

            ViewBag.ValidationSummaryMessage = errorMessage;

            var clientreturn = new Client
            {
                Email = model.Email,
                Password = model.Password,
                Lname = model.Lname,
                Fname = model.Fname
            };

            return View("Register", clientreturn);
        }
    }

}

