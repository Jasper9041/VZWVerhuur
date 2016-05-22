using System;
using System.Net.Mail;
using System.Web.Mvc;
//using MVCEmail.Models;
using SportsStore.Models.Domain;
using SportsStore.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
       private IProductRepository productRepository;
        private ICustomerRepository customerRepository;

        private List<CartLine> Cartlines { get; set; }
    
        public CartController(IProductRepository productRepository, ICustomerRepository customerRepository)
        {
            this.productRepository = productRepository;
            this.customerRepository = customerRepository;
        }

         public ActionResult Index(Cart cart)
        {
            if (cart.NumberOfItems == 0)
                return View("EmptyCart");
            ViewBag.Total = cart.TotalValue;      
            return View(cart.CartLines);
        }

        public ActionResult Add(int id, int quantity, Cart cart)
        {
            Product product = productRepository.FindById(id);
            if (product != null)
            {
                try {
                    if (quantity > product.MaxAantal)
                    {
                        throw new ArgumentException("Het opgegeven aantal is ongeldig.");
                    }
                }
                catch(ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    TempData["message"] = ex.Message;
                    return RedirectToAction("Index", "Store");

                }
               cart.AddLine(product, quantity);
                TempData["Info"] = "Product " + product.Name + " is toegevoegd aan de envelop.";
            }
            return RedirectToAction("Index", "Store");
        }

      
        public ActionResult Remove(int id, Cart cart)
        {
            Product product = productRepository.FindById(id);
            cart.RemoveLine(product);
            return RedirectToAction("Index");
        }

        public ActionResult Plus(int id, Cart cart)
        {
            cart.IncreaseQuantity(id);
            return RedirectToAction("Index");
        }

        public ActionResult Min(int id, Cart cart)
        {
           cart.DecreaseQuantity(id);
            return RedirectToAction("Index");
        }

       [Authorize(Roles="customer,admin")]
        public ActionResult CheckOut(Cart cart)
        {
            if (cart.NumberOfItems == 0)
                return RedirectToAction("Index", "Store");
            Cartlines = cart.GetCartLines();
            ViewBag.Total = cart.TotalValue;
            return View(new CheckOutViewModel(cart.GetCartLines()));
        }

        [HttpPost]
        public ActionResult CheckOut(CheckOutViewModel CheckOutViewModel,Cart cart)
        {

            try
            {

                Customer c = customerRepository.FindBy(User.Identity.GetUserName());
               string besteldeProducten = null;

                foreach (CartLine line in cart.CartLines)
                {
                    besteldeProducten += "<tr>" + "<td>"+line.Product.Name+"</td>" +"<td>" +line.Quantity+"</td>" + "</tr>";
                }

                DateTime start = CheckOutViewModel.StartDatumHuur;
                DateTime einde = CheckOutViewModel.EindDatumHuur;
                Order o = new Order(start, einde, CheckOutViewModel.Voorwaarden);
                sendMail(besteldeProducten, start, einde);
                TempData["Info"] = "Bedankt voor het plaatsen van uw aanvraag " + User.Identity.GetUserName() + ". We contacteren u zo snel mogelijk.";
                cart.Clear();
                // return View(new CheckOutViewModel(producten));
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
            }


            // TempData["Message"] = "Er is iets misgelopen " + User.Identity.GetUserName();
            // return View(new CheckOutViewModel(producten));
            return View(new CheckOutViewModel(cart.GetCartLines()));

        }

        private string sendMail(string producten, DateTime start,DateTime einde)
        {
            string startS =  start.ToShortDateString();
            string eindS = einde.ToShortDateString();
            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress("thomas.de.wulf@telenet.be"));
            mail.From = new MailAddress("thomas.de.wulf@telenet.be");
            mail.Subject = "Nieuwe aanvraag";
            string Body = "<p>Er is een nieuwe aanvraag voor verhuur binnengekomen.</p><br/> Gegevens klant: "  + User.Identity.GetUserName() +
                "<br/> <p>Gegevens bestelling: <br/>" + "<table border=\"1\" style=\"width: 500px \">"+"<tr><th>Productnaam</th><th>Aantal</th>" + producten + "</table>" +  "<br/>Startdatum uitlenen: " + startS + "<br/>Einddatum uitlenen: " + eindS;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.telenet.be";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            ("t323653", "diluna1");// Enter senders User name and password
            smtp.EnableSsl = true;
            smtp.Send(mail);
            return producten;

        }
        }
    }

