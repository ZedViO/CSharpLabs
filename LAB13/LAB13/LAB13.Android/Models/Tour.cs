using System.ComponentModel.DataAnnotations;

namespace LAB12.Model
{
    public class Tour
    {
        [Key]
        public int TourID { get; set; }
        [Required]
        public string TourName { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required]
        public List<Booking> Bookings { get; set; }
    }
}
