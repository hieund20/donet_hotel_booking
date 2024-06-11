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

        public async Task<List<Hotel>> GetAllAsync(string? filterOn = null, string? filterQuery = null)
        {
            var hotels = _dBContext.Hotels.AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    hotels = hotels.Where(x => x.Name.Contains(filterQuery));
                }

                if (filterOn.Equals("Province", StringComparison.OrdinalIgnoreCase))
                {
                    hotels = hotels.Where(x => x.Province.Contains(filterQuery));
                }
            }

            return await hotels.ToListAsync();
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
