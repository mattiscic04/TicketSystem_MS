using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.ViewModels;

namespace Presentation.Controllers
{
    public class TicketsController : Controller
    {

        private TicketDBRepository _ticketRepository;
        private FlightDbRepository _flightRepository;


        public TicketsController(TicketDBRepository ticketRepository, FlightDbRepository flightRepository)
        {
            _ticketRepository = ticketRepository;
            _flightRepository = flightRepository;

        }

        public IActionResult Index()
        {
            IQueryable<Ticket> list = _ticketRepository.GetTicket().OrderBy(x => x.Id);
            var output = from t in list
                          select new ListTicketVIewModel()
                          {
                              Id = t.Id,
                              Name = t.Name,
                              Surname = t.Surname,
                              Row = t.Row,
                              Column = t.Column,
                              Passport = t.Passport,
                              PricePaid = t.PricePaid,
                              Cancelled = t.Cancelled,
                              FlightName = t.Flight.Id.ToString()
                          };

            return View(output);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            var flights = _flightRepository.GetFlights().ToList();
            BookTicketstViewModel myModel = new BookTicketstViewModel(_flightRepository);
            { 

            }
            return View(myModel);
        }

        [HttpPost]
        public IActionResult Create(BookTicketstViewModel myModel)
        {
            try
            {
                _ticketRepository.Book(new Ticket()
                {
                    Name = myModel.Name,
                    Surname = myModel.Surname,
                    Row = myModel.Row,
                    Column = myModel.Column,
                    Passport = myModel.Passport,

                });

                TempData["message"] = "Ticket saved Successfully";

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["error"] = "Tickets was not saved Successfully";
                return View(myModel);
            }

        }


    }
}
