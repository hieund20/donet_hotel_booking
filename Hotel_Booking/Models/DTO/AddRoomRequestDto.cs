using System.ComponentModel.DataAnnotations;

namespace Hotel_Booking.Models.DTO
{
    public class AddRoomRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string RoomType { get; set; }
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public Guid HotelId { get; set; }
    }
}
