using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.ViewModels
{
    public class ChangeCustomerViewModel
    {
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Email { get; set; }

        public bool isAdmin { get; set; }

        public string Adress { get; set; }
        public ChangeCustomerViewModel()
        {
            
        }

        
    }
}