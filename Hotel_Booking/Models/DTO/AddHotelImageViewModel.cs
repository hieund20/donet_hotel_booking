using System.ComponentModel.DataAnnotations;

namespace Hotel_Booking.Models.DTO
{
    public class AddHotelImageViewModel
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDesciption { get; set; }
        public Guid HotelId { get; set; }
    }
}
