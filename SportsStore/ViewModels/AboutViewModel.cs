using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportsStore.ViewModels
{
    public class AboutViewModel
    {
        [Required(ErrorMessage = "Emailadres is verplicht in te vullen.")]
        [DataType(DataType.EmailAddress,ErrorMessage = "Emailadres is niet correct.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Onderwerp is verplicht in te vullen.")]
        [MaxLength(200,ErrorMessage ="Uw onderwerp is te lang. Gelieve een onderwerp in te geven korter dan 200 karakters.")]
        public string Onderwerp { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Bericht is verplicht in te vullen.")]
        [MinLength(10,ErrorMessage = "Gelieve minstens 10 karakters in te voeren.")]
        public string Bericht { get; set; }

    }
}