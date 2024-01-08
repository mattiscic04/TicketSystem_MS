using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Repositories;
using Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        private readonly TicketDBRepository _ticketRepository;
        private readonly FlightDbRepository _flightDbRepository;


        public AdminController(TicketDBRepository ticketRepository, FlightDbRepository flightDbRepository)
        {
            _ticketRepository = ticketRepository;
            _flightDbRepository = flightDbRepository;
        }

        public IActionResult ListTickets()
        {
            var tickets = _ticketRepository.GetTicket().ToList();
            var viewModelList = tickets.Select(t => new AdminTicketListViewModel
            {
                Name = t.Name,
                Surname = t.Surname,
                Row = t.Row,
                Column = t.Column,
                Cancelled = t.Cancelled,
                PassportNo = t.PassportNo,
                PricePaid = t.PricePaid


            }).ToList();

            return View(viewModelList);
        }

        public IActionResult ListFlights()
        {
            var flights = _flightDbRepository.GetFlights().ToList();
            var viewModelList = flights.Select(f => new AdminFlightViewModel
            {
                Id = f.Id,
                Rows = f.Rows,
                Columns = f.Columns,
                DepartureDate = f.DepartureDate,
                ArrivalDate = f.ArrivalDate,
                CountryFrom = f.CountryTo,
                CountryTo = f.CountryFrom,
                WholesalePrice = f.WholesalePrice,
                CommisionRate = f.CommisionRate,
                retailPrice = f.WholesalePrice * f.CommisionRate,

            }).ToList();

            return View(viewModelList);
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated == false)
            {

                TempData["Error"] = "Access Denied";

                return RedirectToAction("Error", "Home");
            }


            return View();
        }
    }
}

