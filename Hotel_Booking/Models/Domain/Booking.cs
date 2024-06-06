namespace Hotel_Booking.Models.Domains
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime CheckingDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; }

        //Navigation propertiesz    
        public Room Room { get; set; }
    }
}
