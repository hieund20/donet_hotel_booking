using Hotel_Booking.Models.Domains;

namespace Hotel_Booking.Models.DTO
{
    public class HotelWithImageViewDto
    {
        public Hotel Hotel { get; set; }
        public string ImageUrl { get; set; }  
    }
}
