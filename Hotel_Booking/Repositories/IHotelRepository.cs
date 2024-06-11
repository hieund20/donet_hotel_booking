using Hotel_Booking.Models.Domains;

namespace Hotel_Booking.Repositories
{
    public interface IHotelRepository
    {
        Task<List<Hotel>> GetAllAsync(string? filterOn = null, string? filterQuery = null);
        Task<Hotel?> GetByIdAsync(Guid id);
        Task<Hotel> AddNewAsync(Hotel hotel);
        Task<Hotel?> UpdateyIdAsync(Guid id, Hotel hotel);
        Task<Hotel?> DeleteByIdAsync(Guid id);
    }
}
