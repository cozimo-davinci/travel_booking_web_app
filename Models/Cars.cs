using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
	public class Cars
	{
        [Key]
        public int CarId { get; set; } // Primary Key

        [Required]
        public string? Model { get; set; }

        [Required]
        [Display(Name = "Rental Company")]
        public string? RentalCompany { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Price Per Day")]
        public float Price { get; set; }

        public string? ImagePath { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime AvailabilityStartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime AvailabilityEndDate { get; set; }

        // Make CartID nullable
        public int? CartID { get; set; }

        // Navigation property for Cart
        public Cart Cart { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AvailabilityEndDate > AvailabilityStartDate)
            {
                yield return new ValidationResult("Available From must be greater than Available To", new[] { nameof(AvailabilityEndDate), nameof(AvailabilityStartDate) });
            }
        }


    }
}
