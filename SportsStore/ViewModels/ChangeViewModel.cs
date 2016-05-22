using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsStore.Models.Domain;

namespace SportsStore.ViewModels
{
    public class ChangeViewModel
    {
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string VerenigingOfBedrijf { get; set; }
        public string Email { get; set; }

        private Customer customer;

        public ChangeViewModel()
        {
            
        }
        public ChangeViewModel(Customer c)
        {
            this.customer = c;
        }
    }
}