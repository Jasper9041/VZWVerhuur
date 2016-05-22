//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNet.Identity.Owin;
//using SportsStore.Models.Domain;

//namespace SportsStore.Models.DAL
//{


//    public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
//    {
//        private ApplicationUserManager userManager;
//        private ApplicationRoleManager roleManager;
//        protected override void Seed(ApplicationDbContext context)
//        {
//            userManager =
//                HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

//            roleManager =
//                HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
//            InitializeIdentityForEF();
//            base.Seed(context);
//        }

//        public  void InitializeIdentityForEF()
//        {


//            const string name = "admin@hogent.be";
//            const string password = "Admin@1";
//            const string roleName = "admin";

//            //Create Role Admin if it does not exist
//            var role = roleManager.FindByName(roleName);
//            if (role == null)
//            {
//                role = new IdentityRole(roleName);
//                var roleresult = roleManager.Create(role);
//            }

//            //Create user Admin
//            var user = userManager.FindByName(name);
//            if (user == null)
//            {
//                user = new ApplicationUser { UserName = name, Email = name };
//                var result = userManager.Create(user, password);
//                result = userManager.SetLockoutEnabled(user.Id, false);
//            }
//            // Add user admin to Role Admin if not already added
//            var rolesForUser = userManager.GetRoles(user.Id);
//            if (!rolesForUser.Contains(role.Name))
//            {
//                var result = userManager.AddToRole(user.Id, role.Name);
//            }



//            const string roleCustomer = "Customer";

//            //Create Role Student if it does not exist
//            role = roleManager.FindByName(roleCustomer);
//            if (role == null)
//            {
//                role = new IdentityRole(roleCustomer);
//                var roleresult = roleManager.Create(role);
//            }

//        }
//    }
//}
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNet.Identity.Owin;
//using SportsStore.Models.Domain;

//namespace SportsStore.Models.DAL
//{

//    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
//    {
//       
//        protected override void Seed(ApplicationDbContext context)
//        {

//            // InitializeIdentity();
           
//        }

       
//    }
//}

