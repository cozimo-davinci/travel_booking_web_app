using System.ComponentModel.DataAnnotations;

namespace COMP2139_Assignment1.Models
{
    public class Hotels
    {
        [Key]
        public int HotelsId { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string? Type { get; set; }

        public float? Price { get; set; }

        [Required]
        public string? Address { get; set; }

        // rating state will be used for enhancements in Assignment 2 (review feature)
        public string? Rating { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime AvailabilityStartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime AvailabilityEndDate { get; set; }

        public string? ImagePath {  get; set; }

        

        // Make CartID nullable
        public int? CartID { get; set; }

        // Navigation property for Cart
        public Cart Cart { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AvailabilityEndDate > AvailabilityStartDate)
            {
                yield return new ValidationResult("End Date must be greater than Start Date", new[] { nameof(AvailabilityEndDate), nameof(AvailabilityStartDate) });
            }
        }
    }
}
