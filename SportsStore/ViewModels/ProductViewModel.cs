using SportsStore.Models.Domain;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SportsStore.ViewModels
{
    public class ProductViewModel
    {
        [Display(Name= "Artikelcode")]
        public string ArtikelCode { get; set; }
        public int ProductId { get; set; }
        [Display(Name= "Naam")]
        [Required]
        [StringLength(100, ErrorMessage = "De productnaam {0} moet minstens {2} karakters lang zijn.", MinimumLength = 2)]
        public string Name { get; set; }
        [Display(Name = "Beschrijving")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        [Range(0.05, 3000, ErrorMessage ="{0} moet positief zijn")]
        public int Price { get; set; }
        [DisplayName("In stock")]
        public bool InStock { get; set; }
        //[Required]
        //public Availability Availability { get; set; }
        //[DisplayName("Available till")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        //public DateTime? AvailableTill { get; set; }
        [Required]
        [DisplayName("Categorie")]
        public int CategoryId { get; set; }
        //public byte[] ImageData { get; set; }
        //public string ImageMimeType { get; set; }

        //        [DataType(DataType.Upload)]
        // private HttpPostedFileBase ImageUpload { get; set; }


            public int MaxAantal { get; set; }

        public ProductViewModel()
        {
            
        }

        public ProductViewModel(Product p)
        {
            ArtikelCode = p.ArtikelCode;
            ProductId = p.ProductId;
            Name = p.Name;
            Description = p.Description;
            Price = p.Price;
            InStock = p.InStock;
            MaxAantal = p.MaxAantal;
            //Availability = p.Availability;
            //AvailableTill = p.AvailableTill;
            if (p.Category!=null)
                 CategoryId = p.Category.CategoryId;

            //ImageData = p.ImageData;
            //ImageMimeType = p.ImageMimeType;
        }
    }
}