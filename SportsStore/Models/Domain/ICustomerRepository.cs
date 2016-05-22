using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Models.Domain
{
    public interface ICustomerRepository
    {
        Customer FindBy(string customerName);
        void SaveChanges();
        void Add(Customer customer);

        IEnumerable<Customer> FindAll(); 

        //void SetCurrentCustomer(Customer customer);
        //Customer getCurrentCustomer();

        Customer FindById(int id);
        
    }
}
