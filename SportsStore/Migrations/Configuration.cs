using System.Collections.Generic;
using System.Data.Entity.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SportsStore.Models.DAL;
using SportsStore.Models.Domain;

namespace SportsStore.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SportsStore.Models.DAL.VerhuurContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SportsStore.Models.DAL.VerhuurContext";
        }

        protected override void Seed(SportsStore.Models.DAL.VerhuurContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            try
            {
                Category tenten = new Category() { Name = "Tenten" };
                Category keuken = new Category() { Name = "Keuken" };
                Category tafelsEnBanken = new Category() { Name = "Tafels en Banken" };
                var categories = new List<Category> { tenten, keuken, tafelsEnBanken };

                categories.ForEach(c => context.Categories.Add(c));
                tenten.AddProduct("Seniortent", 75, "Grote tent", 10);
                keuken.AddProduct("Pan", 5, "Iets om dingen in te bakken", 10);
                tafelsEnBanken.AddProduct("Tafel", 5, "Ding om andere dingen op te leggen.", 10);
                tenten.AddProduct("Ronde speeltent", 50, "Ronde tent om in te spelen..", 10);
                context.SaveChanges();
                Customer admin = new Customer
                {
                    Naam = "de Wulf",
                    Voornaam = "Thomas",
                    CustomerName = "deWulfThomas",
                    Email = "admin@sportsstore.be",
                    VerenigingBedrijf = "admin"
                };
                InitializeIdentityAndRoles(context);
                base.Seed(context);
                context.Customers.Add(admin);
                context.SaveChanges();
            }

            catch (DbEntityValidationException e)
            {
                string s = "Fout creatie database ";
                foreach (var eve in e.EntityValidationErrors)
                {
                    s += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.GetValidationResult());
                    foreach (var ve in eve.ValidationErrors)
                    {
                        s += String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new Exception(s);
            }
        }

        private void InitializeIdentityAndRoles(VerhuurContext context)
        {
            CreateUserAndRoles("admin@sportsstore.be", "P@ssword1", "admin", context);

        }


        private void CreateUserAndRoles(string email, string password, string roleName, VerhuurContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            //Create user
            ApplicationUser user = userManager.FindByName(email);
            if (user == null)
            {
                user = new ApplicationUser { UserName = email, Email = email, LockoutEnabled = false };
                IdentityResult result = userManager.Create(user, password);
                if (!result.Succeeded)
                    throw new ApplicationException(result.Errors.ToString());
            }

            //Create roles
            IdentityRole role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                IdentityResult result = roleManager.Create(role);
                if (!result.Succeeded)
                    throw new ApplicationException(result.Errors.ToString());
            }

            //Associate user with role
            IList<string> rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                IdentityResult result = userManager.AddToRole(user.Id, roleName);
                if (!result.Succeeded)
                    throw new ApplicationException(result.Errors.ToString());
            }
        }

    }
}
