namespace Hotel_Booking.Models.Domains
{
    public class Room
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string RoomType { get; set; }
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public Guid HotelId { get; set; }

        //Navigation propertiesz    
        public Hotel Hotel { get; set; }
    }
}
