using SportsStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult About(AboutViewModel model)
        {
            if(ModelState.IsValid)
            {
                
              
                MailMessage mail = new MailMessage();
                mail.To.Add(new MailAddress("thomas.de.wulf@telenet.be"));
                mail.From = new MailAddress("thomas.de.wulf@telenet.be");
                mail.Subject = "Bericht via webshop.";
                    string Body = "Er is een nieuw bericht via de webshop:<br/>Van: " + model.Email + "<br/>Onderwerp: " + model.Onderwerp + "<br/>Bericht: " + model.Bericht;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.telenet.be";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                ("username", "password");// Enter senders User name and password. Placeholder text atm. Do not forget at deployment.
                smtp.EnableSsl = true;
                smtp.Send(mail);

                TempData["Info"] = "Uw bericht werd verstuurd.";
                    return RedirectToAction("Index", "Home");
              
            }
            return View();
          
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}