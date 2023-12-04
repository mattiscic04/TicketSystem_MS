using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Ticket
    {
        // Properties (Id, Row, Column, FlightIdFK, Passport, PricePaid, Cancelled)

        public Ticket()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        //public string? Owner { get; set; }

        public int Row { get; set; }
        public int Column { get; set; }


        [ForeignKey(nameof(Flight))]
        public Guid FlightIdFK { get; set; }

        public virtual Flight Flight { get; set; }

        public string Passport { get; set; }
        public double PricePaid { get; set; }
        public bool Cancelled { get; set; }

    }
}