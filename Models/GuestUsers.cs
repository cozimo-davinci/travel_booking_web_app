using System.ComponentModel.DataAnnotations;
namespace bookingPlatform.Models
{
    public class GuestUsers
    {
        [Key]
        public int GuestUserId { get; set; }

        public required string FullName { get; set; }

        public required string Email { get; set; }
    }
}
