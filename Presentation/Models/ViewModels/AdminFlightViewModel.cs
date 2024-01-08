using DataAccess.Repositories;
using Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Presentation.Models.ViewModels
{
    public class AdminFlightViewModel
    {

        public AdminFlightViewModel() { }




        public Guid Id { get; set; }

        [Required]
        public int Rows { get; set; }

        [Required]
        public int Columns { get; set; }

        [Display(Name = "Departure Date")]
        public DateTime DepartureDate { get; set; }

        [Display(Name = "Arrival Date")]
        public DateTime ArrivalDate { get; set; }

        [Display(Name = "Country From")]
        public string CountryFrom { get; set; }

        [Display(Name = "Country To")]
        public string CountryTo { get; set; }
        public double WholesalePrice { get; set; }
        public double CommisionRate { get; set; }

        [Display(Name = "Retail Price")]
        public double retailPrice { get; set; }
        public bool IsFullyBooked { get; set; }

    }
}
