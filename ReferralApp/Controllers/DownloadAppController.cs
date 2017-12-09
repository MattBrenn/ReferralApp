using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ReferralApp.Controllers
{
    public class DownloadAppController : Controller
    {
        // GET: DownloadApp
        public ActionResult Index()
        {
            return View("DownloadApp");
        }
    }
}