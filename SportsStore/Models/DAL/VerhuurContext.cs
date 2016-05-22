using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using SportsStore.Models.Domain;

namespace SportsStore.Models.DAL
{
    public class VerhuurContext : IdentityDbContext<ApplicationUser>
    {
        public VerhuurContext() : base("Verhuur")
        {
        }

        
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }

        //public DbSet<Image> Images { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());

        }

        public static VerhuurContext Create()
        {
            return DependencyResolver.Current.GetService<VerhuurContext>();
        }
    }
}