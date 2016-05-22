using System;
using System.Collections.Generic;


namespace SportsStore.Models.Domain
{
    public class Customer
    {
        #region Properties
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }

        public bool isAdmin { get; set; }

        public string VerenigingBedrijf { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        //public virtual City City { get; set; }

            public string Gemeente { get; set; }
        public string Telefoonnummer { get; set; }
        public List<Order> Orders { get; set; }
        #endregion

        #region Methods

        public Customer()
        {
            Orders = new List<Order>();
        }


        public void PlaceOrder( DateTime start, DateTime eind, bool voorwaarden)
        {
            Orders.Add(new Order(start,eind,voorwaarden));
        }
        #endregion
    }
}