using DataAccess.Repositories;
using Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Presentation.Models.ViewModels
{
    public class BookTicketstViewModel
    {

        public BookTicketstViewModel() { }

        public BookTicketstViewModel(FlightDbRepository flightDbRepository) 
        {
            Flights = flightDbRepository.GetFlights();
        }


        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public IQueryable<Flight> Flights { get; set; }
        public int FlightIdFK { get; set; }

        //public string Passport { get; set; }

        public IFormFile Passport { get; set; }
        public double PricePaid { get; set; }
        public bool Cancelled { get; set; }

    }
}
