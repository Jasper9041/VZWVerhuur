using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SportsStore.Models.Domain;

namespace SportsStore.ViewModels
{
    public class CheckOutViewModel
    {
        public Product Product { get; set; }
        [Display(Name = "Aantal")]
        public int Quantity { get; set; }
        //public int Total { get { return Product.Price * Quantity; } }

        public List<CartLine> Cartlines { get; set; }

        [DisplayName("Startdatum uitlenen")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDatumHuur { get; set; }
        [DisplayName("Einddatum uitlenen")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EindDatumHuur { get; set; }

        [Required(ErrorMessage = "Gelieve de algemene voorwaarden voor verhuur te aanvaarden.")]
        [Display(Name = "Ik ben akkoord met de algemene voorwaarden.")]
        
        public bool Voorwaarden { get; set; }
        public CheckOutViewModel(List<CartLine> cartLine)
        {
            Cartlines = cartLine;
           // Product = cartLine.Product;
            //Quantity = cartLine.Quantity;
            
        }

        public CheckOutViewModel()
        {
        //    if (StartDatumHuur < DateTime.Today.AddDays(7))
        //    {
        //        throw new ArgumentException("Gelieve minstens 7 dagen op voorhand een aanvraag in te dienen");
        //    }
        //    if (StartDatumHuur.AddDays(14) > EindDatumHuur)
        //    {
        //        throw new ArgumentException("Materiaal kan niet langer dan 14 dagen uitgeleend worden");
        //    }
        //    if (!Voorwaarden)
        //    {
        //        throw new ArgumentException("Gelieve de algemene voorwaarden te aanvaarden.");
        //    }
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}