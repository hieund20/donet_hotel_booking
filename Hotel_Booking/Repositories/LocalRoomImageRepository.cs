using Hotel_Booking.Data;
using Hotel_Booking.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Booking.Repositories
{
    public class LocalRoomImageRepository : IRoomImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HotelBookingDBContext _dBContext;

        public LocalRoomImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, HotelBookingDBContext dBContext)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._httpContextAccessor = httpContextAccessor;
            this._dBContext = dBContext;
        }

        public async Task<RoomImage?> DeleteAsync(Guid id)
        {
            var existingRoomImage = await _dBContext.RoomImages.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRoomImage == null)
            {
                return null;
            }

            _dBContext.RoomImages.Remove(existingRoomImage);
            await _dBContext.SaveChangesAsync();
            return existingRoomImage;
        }

        public async Task<List<RoomImage>> GetAllByRoomIdAsync(Guid id)
        {
            var images = await _dBContext.RoomImages.ToListAsync();

            List<RoomImage> result = new List<RoomImage>();

            foreach (var image in images)
            {
                if (image.RoomId == id)
                {
                    result.Add(image);
                }
            }

            return result;
        }

        public async Task<RoomImage?> GetByRoomIdAsync(Guid roomId)
        {
            return await _dBContext.RoomImages.FirstOrDefaultAsync(x => x.RoomId == roomId);
        }

        public async Task<RoomImage?> GetByIdAsync(Guid id)
        {
            return await _dBContext.RoomImages.Include(x => x.Room).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<RoomImage> Upload(RoomImage image)
        {
            if (image.File != null && image.File.Length > 0)
            {
                var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

                //Upload Image to Local Path
                using var stream = new FileStream(localFilePath, FileMode.Create);
                await image.File.CopyToAsync(stream);

                //https://localhost:1234/images/image.jpg
                var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
                image.FilePath = urlFilePath;

                //Add Image to the Image table
                _dBContext.RoomImages.Add(image);
                await _dBContext.SaveChangesAsync();

                return image;
            }
            else
            {
                // Handle when image.File is null or empty
                throw new ArgumentException("No file or empty file provided.");
            }
        }
    }
}
