﻿using Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Presentation.Models.ViewModels
{
    public class ListFlightVIewModel
    {

        public Guid Id { get; set; }

        [Required]
        public int Rows { get; set; }

        [Required]
        public int Columns { get; set; }

        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string CountryFrom { get; set; }
        public string CountryTo { get; set; }
        public double WholesalePrice { get; set; }
        public double CommisionRate { get; set; }

    }
}
