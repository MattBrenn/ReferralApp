using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReferralApp.Models;
using ReferralApp.ServiceReference1;
using ReferralCodeGenerator;
using Customer = ReferralApp.Models.Customer;
using System.Configuration;
using System.Globalization;
using System.Web.UI.WebControls;
using ReferralApp.Extension;

namespace ReferralApp.Controllers
{
    public class InviteController : Controller
    {
        // GET: Invite
        public ActionResult Index(string str )
        {

            if (Request.QueryString["str"] == null)
            {
               return RedirectToAction("Index", "Login", new {loginType = "referrallogin"});
            }

           int num;
           bool res = Int32.TryParse(str, out num);

           int customerid = num;

            try
            {

                CodeGenerator referral = new CodeGenerator();
                var result = referral.GetCodeFromId(customerid);

                AdminClient client = new AdminClient();
                var details = client.GetCustomerSummary(customerid, "76afb1fd-143c-411a-8c8a-4771636a5420");
                var cname = details.Name;


                var customer = new Customer
                {
                    ReferralCode = result
                };

                var setting = ConfigurationManager.AppSettings["DynamicRegisterlink"];

                string registerLink = string.Format(setting, customer.ReferralCode);

                var mail = new Models.Mail
                {
                    UserName = cname,
                    CreditOffered = 5.00m,
                    Message = "I've been using this app for printing my photos called Foto Store. Its super simple to use and they have really quick delivery. They are running a refer a friend offer where you get €" 

                };

                var firstnameonly = mail.UserName.TrimAndReduce();

                @ViewBag.CustomerWelcome = "Hi " + firstnameonly;

                @ViewBag.EmailMessage = mail.Message + mail.CreditOffered + " off your first order of photo prints.\nI thought you might be interested.\nUse the link to get €5.00 of your first order. \n\n" + registerLink + "\n\n\n \n\n\n \n\n\n ";

                @ViewBag.WhatsappMessage = mail.Message + mail.CreditOffered + " off your first order of photo prints. I thought you might be interested. Use the link to get €5.00 of your first order. \n\n" + registerLink + " ";

                @ViewBag.SMSTextMessage = mail.Message + mail.CreditOffered + " off your first order of photo prints. I thought you might be interested. Use the link to get €5.00 of your first order. \n\n" + registerLink + " ";

                return View("Invite");

            }
            catch (Exception ex)
            {
                if (ex.Message == "No customer found")
                {
                    return RedirectToAction("Index", "Login", new {loginType = "referrallogin"});
                }
                Logger.Error("Error getting customer summary", ex);
                throw;
            }

        }       
    }
}