﻿using DataAccess.Interface;
using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.ViewModels;

namespace Presentation.Controllers
{
    public class TicketsController : Controller
    {
        private ITicketRepository _IticketRepository;
        private TicketDBRepository _ticketRepository;
        private FlightDbRepository _flightRepository;


        public TicketsController(TicketDBRepository ticketRepository, FlightDbRepository flightRepository, ITicketRepository IticketRepository)
        {
            _ticketRepository = ticketRepository;
            _flightRepository = flightRepository;
            _IticketRepository = IticketRepository;

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
                             CountryTo = f.CountryFrom,
                             retailPrice = f.WholesalePrice*f.CommisionRate,
                         };



            return View(output);
        }

        [HttpGet]
        public IActionResult Create([FromRoute] Guid Id)
        {
            var flights = _flightRepository.GetFlights().ToList();
            var selectedFlight = flights.FirstOrDefault(f => f.Id == Id);

            if (selectedFlight == null)
            {
                // Handle the case where the selected flight is not found
                TempData["error"] = "Invalid FlightId provided.";
                return RedirectToAction("Index");
            }

            var myModel = new BookTicketstViewModel(_flightRepository)
            {
                SelectedFlight = selectedFlight
            };

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


                ////Check if the selected flight is fully booked or canceled
                //var existingTickets = _ticketRepository.GetTicketsForFlight(Id);
                //if (existingTickets.Any(t => !t.Cancelled) && existingTickets.Count() >= (existingFlight.Rows * existingFlight.Columns))
                //{
                //    TempData["error"] = "The selected flight is fully booked.";
                //    return RedirectToAction("Index");
                //}

                // Check if the flight departure date is in the future
                if (existingFlight.DepartureDate <= DateTime.Now)
                {
                    TempData["error"] = "The selected flight has already departed or is in the past.";
                    return RedirectToAction("Index");
                }

                if (myModel.Passport == null || myModel.Passport.Length == 0)
                {
                    TempData["error"] = "Please Upload an Image of your Passport";

                    var flights = _flightRepository.GetFlights().ToList();
                    var selectedFlight = flights.FirstOrDefault(flight => flight.Id == Id);
                    myModel.SelectedFlight = selectedFlight;
                    return View(myModel);
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

                // Calculate PricePaid after commission on WholesalePrice
                double commissionRate = existingFlight.CommisionRate;
                double wholesalePrice = existingFlight.WholesalePrice;
                double pricePaid = wholesalePrice + (wholesalePrice * commissionRate);

                if (myModel.Row <= 0 || myModel.Column <= 0)
                {
                    TempData["error"] = "Please select another seat";

                    var flights = _flightRepository.GetFlights().ToList();

                    var selectedFlight = flights.FirstOrDefault(flight => flight.Id == Id);

                    myModel.SelectedFlight = selectedFlight;
                    return View(myModel);
                }
                if (myModel.PassportNo == null)
                {
                    TempData["error"] = "Please enter a Passport Number";

                    var flights = _flightRepository.GetFlights().ToList();

                    var selectedFlight = flights.FirstOrDefault(flight => flight.Id == Id);

                    myModel.SelectedFlight = selectedFlight;
                    return View(myModel);
                }
                if (_ticketRepository.CheckBookedSeats(Id, myModel.Row, myModel.Column))
                {
                    TempData["error"] = "Seat is already Booked, Please choose another seat";

                    var flights = _flightRepository.GetFlights().ToList();

                    var selectedFlight = flights.FirstOrDefault(flight => flight.Id == Id);

                    myModel.SelectedFlight = selectedFlight;
                    return View(myModel);
                }

                if (myModel.Name == null || myModel.Surname == null)
                {
                    TempData["error"] = "Please Enter your Full Name";

                    var flights = _flightRepository.GetFlights().ToList();

                    var selectedFlight = flights.FirstOrDefault(flight => flight.Id == Id);

                    myModel.SelectedFlight = selectedFlight;
                    return View(myModel);
                }

                

                var PassengerFound = _ticketRepository.GetTicket().SingleOrDefault(x => x.PassportNo == myModel.PassportNo);
                if (PassengerFound == null)
                {
                    _ticketRepository.Book(new Ticket()
                    {
                        FlightIdFK = Id,
                        Name = myModel.Name,
                        Surname = myModel.Surname,
                        Row = myModel.Row,
                        Column = myModel.Column,
                        PassportNo = myModel.PassportNo,
                        Passport = relativePath,
                        PricePaid = pricePaid,
                        //Owner = User.Identity.Name

                    });

                    TempData["message"] = "Ticket booked Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "A Passenger with the same Passport Number is already booked";

                    var flights = _flightRepository.GetFlights().ToList();
                    var selectedFlight = flights.FirstOrDefault(flight => flight.Id == Id);
                    myModel.SelectedFlight = selectedFlight;
                    return View(myModel);
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = "Tickets was not booked Successfully";
                return View(myModel);
            }

        }

        //public IActionResult Delete(Guid id)
        //{
        //    try
        //    {
        //        if (User.Identity.Name == _ticketRepository.GetTicket(id).Owner)
        //        {
        //            _ticketRepository.CancelTicket(id);
        //            TempData["message"] = "Ticket cancelled successfully";
        //        }
        //        else
        //        {
        //            TempData["error"] = "Access denied";
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["error"] = "Tickets was not Cancelled; Input might have been tampered and Ticket not found";
        //    }

        //    return RedirectToAction("Index");
        //}


    }
}
