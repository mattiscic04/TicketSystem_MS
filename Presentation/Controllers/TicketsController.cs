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
            // Get the current time
            DateTime currentTime = DateTime.Now;

            // Filter out flights that are in the past
            IQueryable<Flight> list = _flightRepository.GetFlights()
                .Where(x => x.DepartureDate > currentTime)
                .OrderBy(x => x.Id);

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

        [HttpGet]
        public IActionResult Create() 
        {
            var flights = _flightRepository.GetFlights().ToList();
            BookTicketstViewModel myModel = new BookTicketstViewModel(_flightRepository);
            {
                //Flights = flights;
            }
            return View(myModel);
        }

        [HttpPost]
        public IActionResult Create(BookTicketstViewModel myModel, [FromServices] IWebHostEnvironment host, [FromRoute] Guid Id)
        {
            string relativePath = "";
            try
            {
                // Validate that the provided FlightId exists in the Flights table
                var existingFlight = _flightRepository.GetFlight(Id);
                if (existingFlight == null)
                {
                    TempData["error"] = "Invalid FlightId provided.";
                    return RedirectToAction("Index");
                }

                string filename = Guid.NewGuid() + Path.GetExtension(myModel.Passport.FileName);

                string absolutePath = host.WebRootPath + @"\images\" + filename;

                relativePath = @"/images/" + filename;

                using (FileStream fs = new FileStream(absolutePath, FileMode.CreateNew))
                {
                    myModel.Passport.CopyTo(fs);
                    fs.Flush();
                    fs.Close();
                }

                _ticketRepository.Book(new Ticket()
                {
                    FlightIdFK = Id,
                    Name = myModel.Name,
                    Surname = myModel.Surname,
                    Row = myModel.Row,
                    Column = myModel.Column,
                    Passport = relativePath
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
