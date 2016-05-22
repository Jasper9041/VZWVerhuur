using System.Linq;

namespace SportsStore.Models.Domain
{
    public interface ICategoryRepository
    {
        IQueryable<Category> FindAll();
        Category FindById(int categoryId);
        Category FindByName(string naam);
        void Add(Category category);
        void SaveChanges();
    }
}
