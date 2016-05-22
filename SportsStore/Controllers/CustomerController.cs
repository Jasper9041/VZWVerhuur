using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SportsStore.Models.Domain;
using SportsStore.ViewModels;

namespace SportsStore.Controllers
{
    [Authorize(Roles = "admin")]
    public class CustomerController : Controller
    {
        private ICustomerRepository customerRepository;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ICustomerRepository CustomerRepository
        {
            get
            {
                return customerRepository ?? (ICustomerRepository)DependencyResolver.Current.GetService(typeof(ICustomerRepository));
            }
            private set { customerRepository = value; }
        }
        public CustomerController(ICustomerRepository customerRepository)
        {
           this.customerRepository = customerRepository;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Customer
        public ActionResult Index()
        {
            IEnumerable<Customer> customers = customerRepository.FindAll();
            return View(customers);
        }

        public ActionResult Edit(int id)
        {
            bool isAdmin = false;
        Customer c = customerRepository.FindById(id);
            var userManager =
                System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
         var   roleManager =
               System.Web.HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();

            ApplicationUser user = userManager.FindByName(c.Email);
            IList<string> rolesForUser = userManager.GetRoles(user.Id);
            IdentityRole role = roleManager.FindByName("admin");
            if (rolesForUser.Contains(role.Name))
                isAdmin = true;
                
                return View(new ChangeCustomerViewModel{Email = c.Email, Voornaam = c.Voornaam,Naam = c.Naam,isAdmin = isAdmin, Adress = c.Adress +" " +  c.Gemeente});
        }

        [HttpPost]
        public ActionResult Edit(int id,ChangeCustomerViewModel model)
        {

            Customer c = CustomerRepository.FindById(id);
            bool isAdmin = model.isAdmin;
            
            c.isAdmin = model.isAdmin;
            
            var  userManager =
                System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByName(c.Email);
            if (isAdmin)
            {
                userManager.RemoveFromRole(user.Id, "customer");
                userManager.AddToRole(user.Id, "admin");
               
            }
            else
            {
                userManager.RemoveFromRole(user.Id, "admin");
                userManager.AddToRole(user.Id, "customer");
            }
            TempData["info"] = "Gegevens werden aangepast";
            return RedirectToAction("Index");
        }
    }
}