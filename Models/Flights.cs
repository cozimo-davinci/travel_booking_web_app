using COMP2139_Assignment1.Models;
using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
    
        public class Flights
        {
        [Key]
        public int FlightId { get; set; }

        [Required]
        public required string Airline { get; set; }

        [Required]
        public required string FlightCode { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DepartureTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ArrivalTime { get; set; }

        [Required]
        public float TotalFlightTime { get; set; }

        [Required]
        public required string Origin { get; set; }

        [Required]
        public required string Destination { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public float EconomyPrice { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public float BusinessPrice { get; set; }

        [Required]
        public int MaxPassengersTotal { get; set; }

        public int? PassengersBookedTotal { get; set; }

        [Required]
        public int MaxPassengersEconomy { get; set; }

        public int? PassengersBookedEconomy { get; set; }

        [Required]
        public int MaxPassengersBusiness { get; set; }

        public int? PassengersBookedBusiness { get; set; }

        public List<FlightCart> FlightCarts { get; set; }
    }
    }
