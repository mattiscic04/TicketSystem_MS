using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Flight
    {

        // Properties (Id, Row, Column, FlightIdFK, Passport, PricePaid, Cancelled)

        public Flight()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }


        [ForeignKey(nameof(Flight))]
        public Guid FlightIdFK { get; set; }
        public Ticket Ticket { get; set; }

        public string Passport { get; set; }
        public double PricePaid { get; set; }
        public bool Cancelled { get; set; }
    }
}
