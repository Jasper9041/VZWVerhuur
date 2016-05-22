using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SportsStore.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        [DisplayName("Naam")]
        public string Name { get; set; }
    }
}