namespace Hotel_Booking.Models.DTO
{
    public class HomeViewDto
    {
        public List<HotelWithImageViewDto> HotelWithImageViewDtos { get; set; }
        public List<Province> Provinces { get; set; }
        public string HotelName { get; set; }
        public int HotelCapacity { get; set; }
    }
}
