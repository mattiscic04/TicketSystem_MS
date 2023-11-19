using DataAccess.DataContext;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class FlightDbRepository
    {
        private BookingFlightContext _BookingFlightContext;

        public FlightDbRepository(BookingFlightContext BookingFlightContext)
        {
            _BookingFlightContext = BookingFlightContext;
        }

        public List<Flight> GetFlights()
        {
            return _BookingFlightContext.Flights.ToList();
        }

        public Flight GetFlight(Guid flightId)
        {
            return _BookingFlightContext.Flights.FirstOrDefault(f => f.Id == flightId);
        }


    }
}
