using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Booking.Models.Domains
{
    public class HotelImage
    {
        public Guid Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDesciption { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
        public Guid HotelId { get; set; }

        //Navigation propertiesz    
        public Hotel Hotel { get; set; }
    }
}
