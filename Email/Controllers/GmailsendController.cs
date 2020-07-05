using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Email.Models;
using System.Net.Http;



namespace Email.Controllers
{
    public class GmailsendController : Controller
    {
        // GET: Gmailsend
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult index(EmailClass ec)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress= new Uri("http://localhost:62034/api/Email");

            var consumewebapi = hc.PostAsJsonAsync<EmailClass>("Email", ec);
            consumewebapi.Wait();
            var sendmail = consumewebapi.Result;
            if (sendmail.IsSuccessStatusCode)
            {
                ViewBag.message = "La notificación ha sido enviada exitosamente!";
            }
            return View();

        } 
        
    }
}