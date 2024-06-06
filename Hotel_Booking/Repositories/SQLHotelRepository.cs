using Hotel_Booking.Data;
using Hotel_Booking.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Booking.Repositories
{
    public class SQLHotelRepository : IHotelRepository
    {
        private readonly HotelBookingDBContext _dBContext;

        public SQLHotelRepository(HotelBookingDBContext dBContext)
        {
            this._dBContext = dBContext;
        }

        public async Task<Hotel> AddNewAsync(Hotel hotel)
        {
            await _dBContext.Hotels.AddAsync(hotel);
            await _dBContext.SaveChangesAsync();
            return hotel;
        }

        public async Task<Hotel?> DeleteByIdAsync(Guid id)
        {
            var existingHotel = await _dBContext.Hotels.FirstOrDefaultAsync(x => x.Id == id);

            if (existingHotel == null)
            {
                return null;
            }

            _dBContext.Hotels.Remove(existingHotel);
            await _dBContext.SaveChangesAsync();
            return existingHotel;
        }

        public async Task<List<Hotel>> GetAllAsync()
        {
            var hotels = await _dBContext.Hotels.ToListAsync();
            return hotels;
        }

        public async Task<Hotel?> GetByIdAsync(Guid id)
        {
            var hotel = await _dBContext.Hotels.FirstOrDefaultAsync(x => x.Id == id);

            if (hotel == null)
            {
                return null;
            }

            return hotel;
        }

        public async Task<Hotel?> UpdateyIdAsync(Guid id, Hotel hotel)
        {
            var existingHotel = await _dBContext.Hotels.FirstOrDefaultAsync(x => x.Id == id);

            if (existingHotel == null)
            {
                return null;
            }

            existingHotel.Name = hotel.Name;
            existingHotel.Address = hotel.Address;
            existingHotel.Description = hotel.Description;
            existingHotel.Phone = hotel.Phone;
            existingHotel.Email = hotel.Email;

            await _dBContext.SaveChangesAsync();

            return existingHotel;
        }
    }
}
