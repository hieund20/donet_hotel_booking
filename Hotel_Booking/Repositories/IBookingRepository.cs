using Hotel_Booking.Models.Domains;

namespace Hotel_Booking.Repositories
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(Guid id);
        Task<Booking> AddNewAsync(Booking booking);
        Task<Booking?> UpdateyIdAsync(Guid id, Booking booking);
        Task<Booking?> DeleteByIdAsync(Guid id);
        Task<bool> DeleteRangeByUserIdAsync(string userId);
    }
}
