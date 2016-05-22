using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SportsStore.Models.Domain;

namespace SportsStore.Models.DAL
{
    public class CustomerRepository : ICustomerRepository
    {
        private VerhuurContext context;
        private DbSet<Customer> customers;
        //private Customer currentCustomer;
        //public Customer CurrenCustomer { get; set; }
        public CustomerRepository(VerhuurContext context)
        {
            this.context = context;
            this.customers = context.Customers;
        } 
        public Customer FindBy(string customerName)
        {
            return customers.SingleOrDefault(c => c.Email.Equals(customerName));

        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Add(Customer customer)
        {
            customers.Add(customer);
        }

        public IEnumerable<Customer> FindAll()
        {
            return customers.OrderBy(c => c.Email);
        }

        //public void SetCurrentCustomer(Customer customer)
        //{
        //    CurrenCustomer = customer;
        //}

        //public Customer getCurrentCustomer()
        //{
        //    return CurrenCustomer;
        //}

        public Customer FindById(int id)
        {
            return customers.SingleOrDefault(c => c.CustomerId.Equals(id));
        }

        
    }
   

    }