using Hotel_Booking.Models.Domains;

namespace Hotel_Booking.Repositories
{
    public interface IHotelImageRepository
    {
        Task<HotelImage> Upload(HotelImage image);
        Task<List<HotelImage>> GetAllByHotelIdAsync(Guid id);
        Task<HotelImage?> GetByIdAsync(Guid id);
        Task<HotelImage?> DeleteAsync(Guid id);
        Task<HotelImage?> GetByHotelIdAsync(Guid hotel);
    }
}
