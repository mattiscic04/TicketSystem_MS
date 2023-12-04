using DataAccess.DataContext;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class TicketDBRepository
    {
        private BookingFlightContext _BookingFlightContext;

        public TicketDBRepository(BookingFlightContext BookingFlightContext)
        {
            _BookingFlightContext = BookingFlightContext;
        }

        public IQueryable<Ticket> GetTicket()
        {
            return _BookingFlightContext.Tickets;
        }

        public Ticket? GetTicket(Guid id)
        {
            return _BookingFlightContext.Tickets.SingleOrDefault(t => t.Id == id);
        }

        public void Book(Ticket ticket)
        {

            // This method is to check if double booking is made
            if (_BookingFlightContext.Tickets.Any(t => t.Row == ticket.Row && t.Column == ticket.Column && t.FlightIdFK == ticket.FlightIdFK))
            {
                throw new InvalidOperationException("The seat you chose is already booked");
            }


            _BookingFlightContext.Tickets.Add(ticket);
            _BookingFlightContext.SaveChanges();
        }

        public void CancelTicket(Guid Id)
        {
            var ticket = _BookingFlightContext.Tickets.Find(Id);
            if (ticket != null)
            {
                ticket.Cancelled = true;
                _BookingFlightContext.SaveChanges();
            }
            else
            {
                throw new Exception("No tickets to cancel");
            }
        }

        public List<Ticket> GetTickets(Guid flightId)
        {
            return _BookingFlightContext.Tickets.Where(t => t.FlightIdFK == flightId).ToList();
        }



        public List<Ticket> GetTicketsForFlight(Guid flightId)
        {
            return _BookingFlightContext.Tickets
                .Include(t => t.FlightIdFK)
                .Where(t => t.FlightIdFK == flightId)
                .ToList();
        }
    }
}

