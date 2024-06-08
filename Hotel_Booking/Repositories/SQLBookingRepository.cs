using Hotel_Booking.Data;
using Hotel_Booking.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Booking.Repositories
{
    public class SQLBookingRepository : IBookingRepository
    {
        private readonly HotelBookingDBContext _dBContext;

        public SQLBookingRepository(HotelBookingDBContext dBContext)
        {
            this._dBContext = dBContext;
        }

        public async Task<Booking> AddNewAsync(Booking booking)
        {
            await _dBContext.Bookings.AddAsync(booking);
            await _dBContext.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking?> DeleteByIdAsync(Guid id)
        {
            var existingBooking = await _dBContext.Bookings.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBooking == null)
            {
                return null;
            }

            _dBContext.Bookings.Remove(existingBooking);
            await _dBContext.SaveChangesAsync();
            return existingBooking;
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            var bookings = await _dBContext.Bookings.ToListAsync();
            return bookings;
        }

        public async Task<Booking?> GetByIdAsync(Guid id)
        {
            var booking = await _dBContext.Bookings.FirstOrDefaultAsync(x => x.Id == id);

            if (booking == null)
            {
                return null;
            }

            return booking;
        }

        public async Task<Booking?> UpdateyIdAsync(Guid id, Booking booking)
        {
            var existingBooking = await _dBContext.Bookings.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBooking == null)
            {
                return null;
            }

            existingBooking.CheckingDate = booking.CheckingDate;
            existingBooking.CheckOutDate = booking.CheckOutDate;
            existingBooking.Status = booking.Status;

            await _dBContext.SaveChangesAsync();

            return existingBooking;
        }
    }
}
