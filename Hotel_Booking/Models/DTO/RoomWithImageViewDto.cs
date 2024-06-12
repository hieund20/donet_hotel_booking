using Hotel_Booking.Models.Domains;

namespace Hotel_Booking.Models.DTO
{
    public class RoomWithImageViewDto
    {
        public Room Room { get; set; }
        public string ImageUrl { get; set; }
    }
}
