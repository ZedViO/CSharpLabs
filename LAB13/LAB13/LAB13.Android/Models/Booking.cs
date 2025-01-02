using System.ComponentModel.DataAnnotations;

namespace LAB12.Model
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }
        [Required]
        public int TourID { get; set; }
        [Required]
        public int ClientID { get; set; }
        [Required]
        public DateTime BookingDate { get; set; }
        [Required]
        public Tour Tour { get; set; }
        [Required]
        public Client Client { get; set; }
    }
}
