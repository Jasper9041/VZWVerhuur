using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SportsStore.Models.Domain
{
    public  class Order
    {
        #region Properties
     public int OrderId { get; set; }   
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDatumHuur { get; set; }
    
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EindDatumHuur { get; set; }

        [Required(ErrorMessage = "Gelieve de algemene voorwaarden voor verhuur te aanvaarden.")]
        [Display(Name = "Ik heb de algemene voorwaarden gelezen en ga hiermee akkoord.")]

        public bool Voorwaarden { get; set; }

        #endregion

        #region Constructors
        public Order(DateTime start, DateTime eind, bool voorwaarden)
        {
            if (start < DateTime.Today || eind < DateTime.Today)
            {
                throw new ArgumentException("Datums moeten in de toekomst liggen");
            }
            if (start < DateTime.Today.AddDays(7))
            {
                throw new ArgumentException("Gelieve minstens 7 dagen op voorhand een aanvraag in te dienen");
            }
          
            if (!voorwaarden)
            {
                throw new ArgumentException("Gelieve de algemene voorwaarden te aanvaarden.");
            }
            this.StartDatumHuur = start;
            this.EindDatumHuur = eind;
            this.Voorwaarden = voorwaarden;

        }

       
        #endregion

       
        

    }
}