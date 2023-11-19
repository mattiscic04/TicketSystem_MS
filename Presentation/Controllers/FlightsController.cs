using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Presentation.Models.ViewModels;

namespace Presentation.Controllers
{
    public class FlightsController : Controller
    {
        private TicketDBRepository _ticketRepository;
        private FlightDbRepository _flightRepository;


        public FlightsController(TicketDBRepository ticketRepository, FlightDbRepository flightRepository)
        {
            _ticketRepository = ticketRepository;
            _flightRepository = flightRepository;

        }

        public IActionResult Index()
        {
            IQueryable<Flight> list = _flightRepository.GetFlights().OrderBy(x => x.Id);
            var output = from f in list
                         select new ListFlightVIewModel()
                         {
                             Id = f.Id,
                             Rows = f.Rows,
                             Columns = f.Columns,
                             DepartureDate = f.DepartureDate,
                             ArrivalDate = f.ArrivalDate,
                             CountryFrom = f.CountryTo,
                             CountryTo = f.CountryFrom
                         };

            return View(output);
        }

        public IActionResult Details(Guid id)
        {
            var ticket = _ticketRepository.GetTicket(id);
            if (ticket == null)
            {
                TempData["error"] = "Product was not found";
                return RedirectToAction("Index");
            }
            else
            {
                ListTicketVIewModel model = new ListTicketVIewModel()
                {
                    Id = ticket.Id,
                              Name = ticket.Name,
                              Surname = ticket.Surname,
                              Row = ticket.Row,
                              Column = ticket.Column,
                              Passport = ticket.Passport,
                              PricePaid = ticket.PricePaid,
                              Cancelled = ticket.Cancelled,
                              FlightName = ticket.Flight.Id.ToString()
                };


                return View(ticket);
            }
        }


    }
}
