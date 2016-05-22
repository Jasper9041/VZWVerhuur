using System.Data.Entity;
using System.Linq;
using SportsStore.Models.Domain;

namespace SportsStore.Models.DAL
{
    public class CategoryRepository:ICategoryRepository
    {
        private VerhuurContext context ;
        private readonly DbSet<Category> categories;

        public CategoryRepository(VerhuurContext context)
        {
            this.context = context;
            categories = context.Categories;
        }

        public IQueryable<Category> FindAll()
        {
            return categories.OrderBy(p => p.Name);
        }

        public Category FindById(int categoryId)
        {
            return categories.Find(categoryId);
        }

        public Category FindByName(string naam)
        {
            return categories.SingleOrDefault(c => c.Name.Equals(naam));
        }

        public void Add(Category category)
        {
            categories.Add(category);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}