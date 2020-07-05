using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Email.Controllers
{
    public class HomeController : Controller
    {

        private static string ApiKey = "AIzaSyApMgu81Wsf9rw4Q_62Am1B9kP1CaRzzI4";
        private static string AuthEmail = "";
        private static string AuthPassword = "";
        private static string Bucket = "https://asp-mvc-51022.firebaseio.com/";

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
