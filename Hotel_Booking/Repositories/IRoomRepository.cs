using Hotel_Booking.Models.Domains;

namespace Hotel_Booking.Repositories
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAllAsync();
        Task<Room?> GetByIdAsync(Guid id);
        Task<Room> AddNewAsync(Room room);
        Task<Room?> UpdateyIdAsync(Guid id, Room room);
        Task<Room?> DeleteByIdAsync(Guid id);
    }
}
