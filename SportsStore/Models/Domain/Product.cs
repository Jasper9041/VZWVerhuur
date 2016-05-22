using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models.Domain
{
    public  class Product
    {
        [DisplayName("Artikelcode")]
        public string ArtikelCode { get; set; }
        public int ProductId { get;  set; }
        [DisplayName("Naam")]
        public string Name { get; set; }
        [DisplayName("Beschrijving")]
        public string Description { get; set; }
        [DisplayName("Prijs")]
        public int Price { get; set; }
        [DisplayName("In stock")]
        public bool InStock { get; set; }
       // public DateTime? AvailableTill { get; set; }
        //public Availability Availability { get; set; }
        [DisplayName("Categorie")]
        public virtual Category Category { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        
        public int MaxAantal { get; set; }

        public Product()
        {
           // Availability = Availability.ShopAndOnline;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Product)
                if ((obj as Product).ProductId == ProductId)
                    return true;
            return false;
        }

        public override int GetHashCode()
        {
            return ProductId;
        }
    }

 
}