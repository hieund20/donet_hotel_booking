using Hotel_Booking.Data;
using Hotel_Booking.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Booking.Repositories
{
    public class LocalHotelImageRepository : IHotelImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HotelBookingDBContext _dBContext;

        public LocalHotelImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, HotelBookingDBContext dBContext)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._httpContextAccessor = httpContextAccessor;
            this._dBContext = dBContext;
        }

        public async Task<HotelImage?> DeleteAsync(Guid id)
        {
            var existingHotelImage = await _dBContext.HotelImages.FirstOrDefaultAsync(x => x.Id == id);

            if (existingHotelImage == null)
            {
                return null;
            }

            _dBContext.HotelImages.Remove(existingHotelImage);
            await _dBContext.SaveChangesAsync();
            return existingHotelImage;
        }

        public async Task<List<HotelImage>> GetAllByHotelIdAsync(Guid id)
        {
            var images = await _dBContext.HotelImages.ToListAsync();

            List<HotelImage> result = new List<HotelImage>();

            foreach (var image in images)
            {
                if (image.HotelId == id)
                {
                    result.Add(image);
                }
            }

            return result;
        }

        public async Task<HotelImage?> GetByIdAsync(Guid id)
        {
            return await _dBContext.HotelImages.Include(x => x.Hotel).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<HotelImage> Upload(HotelImage image)
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
                _dBContext.HotelImages.Add(image);
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
