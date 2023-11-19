using Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Presentation.Models.ViewModels
{
    public class ListTicketVIewModel
    {

        public Guid Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public string FlightName { get; set; }
        public string Passport { get; set; }
        public double PricePaid { get; set; }
        public bool Cancelled { get; set; }

    }
}
