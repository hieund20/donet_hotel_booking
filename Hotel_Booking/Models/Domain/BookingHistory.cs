namespace Hotel_Booking.Models.Domains
{
    public class BookingHistory
    {
        public Guid Id { get; set; }
        public Guid BookingID { get; set; }
        public DateTime StatusChangeDate { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
    }
}
