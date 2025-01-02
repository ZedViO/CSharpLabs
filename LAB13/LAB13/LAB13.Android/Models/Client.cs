using System.ComponentModel.DataAnnotations;

namespace LAB12.Model
{
    public class Client
    {
        [Key]
        public int ClientID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Address { get; set; }

        [Required]
        public List<Booking> Bookings { get; set; }
    }
}
