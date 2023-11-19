using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.ViewModels;

namespace Presentation.Controllers
{
    public class TicketsController : Controller
    {

        private TicketDBRepository _ticketRepository;

        public TicketsController(TicketDBRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public IActionResult Index()
        {
            IQueryable<Ticket> list = _ticketRepository.GetTicket().OrderBy(x => x.Id);
            var output = from t in list
                          select new ListTicketVIewModel()
                          {
                              Id = t.Id,
                              Row = t.Row,
                              Column = t.Column,
                              Passport = t.Passport,
                              PricePaid = t.PricePaid,
                              Cancelled = t.Cancelled,
                              FlightName = t.Flight.Id.ToString()
                          };

            return View(output);
        }

    }
}
