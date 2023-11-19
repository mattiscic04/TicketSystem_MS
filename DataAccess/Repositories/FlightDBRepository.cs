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

        public IQueryable<Flight> GetFlights()
        {
            return _BookingFlightContext.Flights;
        }
        public Flight? GetFlight(Guid id)
        {
            return _BookingFlightContext.Flights.SingleOrDefault(t => t.Id == id);
        }


        public List<Flight> GetFlights(Guid flightId)
        {
            return _BookingFlightContext.Flights.ToList();
        }


    }
}
