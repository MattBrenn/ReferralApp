using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ReferralApp.Models;
using ReferralApp.ServiceReference1;
using ReferralCodeGenerator;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RestSharp;
using Customer = ReferralApp.ServiceReference1.Customer;

namespace ReferralApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index(string loginType)
        {
            ViewBag.LoginHello = "";
            ViewBag.LoginRedeemWelcome = "";
            if (loginType == "redeemlogin")
            {
                ViewBag.LoginHello = "Welcome Back!";
                ViewBag.LoginRedeemWelcome = "Let's get you all that lovely free credit you've earned.";
            }
            if (loginType == "referrallogin")
            {
                ViewBag.LoginHello = "Welcome Back!";
                ViewBag.LoginRedeemWelcome = "";
            }
            if (loginType == "normallogin")
            {
                ViewBag.LoginHello = "Welcome Back!";
                ViewBag.LoginRedeemWelcome = "";
            }

            return View("Login");
        }


        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            try
            {
                var apisetting = ConfigurationManager.AppSettings["DynamicApilink"];

                var restClient = new RestClient(apisetting);

                RestRequest request = new RestRequest("login", Method.POST);
                request.AddHeader("Content-type", "application/json; charset=utf-8");

                var custo = new Models.Customer
                {
                    //Name = customer.Name,
                    EmailAdddress = email,
                    Password = password,
                    //CustomerId = customer.CustomerId
                };



                var json = JsonConvert.SerializeObject(custo);
                request.AddParameter("text/json", json, ParameterType.RequestBody);
                var response = restClient.Execute<Customer>(request);

                var setting = ConfigurationManager.AppSettings["DynamicInvitelink"];
                string inviteLink = string.Format(setting, response.Data.CustomerId);
                var setting2 = ConfigurationManager.AppSettings["DynamicRedeemlink"];
                string redeemLink = string.Format(setting2, response.Data.CustomerId);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var loginType = Request.QueryString["loginType"];

                    switch (loginType.ToLower())
                    {
                        case "referrallogin":
                            return Redirect(inviteLink);
                        case "redeemlogin":
                            return Redirect(redeemLink);
                        default:
                            return Redirect("http://fotostore.ie/");

                    }
                }
                //var error = JsonConvert.DeserializeObject<RestError>(response.Content);
                throw new Exception();
            }

            catch (Exception ex)
            {
                ViewBag.exceptionthrownbool = true;

                ViewBag.ValidationSummaryMessage = "Email or Password is incorrect";

                var clientreturn = new Client
                {
                    Email = email,
                    Password = password
                };
                return View(clientreturn);
            }
        }

    }
}








