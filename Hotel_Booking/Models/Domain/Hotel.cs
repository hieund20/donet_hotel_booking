namespace Hotel_Booking.Models.Domains
{
    public class Hotel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Nation { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
