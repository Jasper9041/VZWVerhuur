using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Models.Domain;

namespace SportsStore.ViewModels
{
    public class CartLineViewModel
    {
        public int ItemId { get; private set; }

        [Display(Name = "Aantal")]
        public int Quantity { get; private set; }

        [Display(Name = "Bier")]
        public String Item { get; private set; }

        [Display(Name = "Prijs")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; private set; }

        [Display(Name = "SubTotaal")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal SubTotal { get; private set; }

        public CartLineViewModel(CartLine cartLine)
        {
            ItemId = cartLine.Product.ProductId;
            Quantity = cartLine.Quantity;
            Item = cartLine.Product.Name;
            Price = cartLine.Product.Price;
            SubTotal = cartLine.Total;
        }
    }


    
}