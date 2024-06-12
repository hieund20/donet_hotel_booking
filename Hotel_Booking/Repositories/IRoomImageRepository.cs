using Hotel_Booking.Models.Domains;

namespace Hotel_Booking.Repositories
{
    public interface IRoomImageRepository
    {
        Task<RoomImage> Upload(RoomImage image);
        Task<List<RoomImage>> GetAllByRoomIdAsync(Guid id);
        Task<RoomImage?> GetByIdAsync(Guid id);
        Task<RoomImage?> DeleteAsync(Guid id);
        Task<RoomImage?> GetByRoomIdAsync(Guid roomId);
    }
}
