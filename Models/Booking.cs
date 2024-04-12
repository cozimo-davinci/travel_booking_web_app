using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; } // Primary Key

        public int? FlightId { get; set; }
        public int? HotelId { get; set; }
        public int? RentalId { get; set; }

        [Required]
        public required int GuestUsedId { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }

        public Hotels? Hotels { get; set; }
    }
}
