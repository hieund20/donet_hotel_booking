using Hotel_Booking.Models.Domains;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Booking.Models.Domains
{
    public class RoomImage
    {
        public Guid Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDesciption { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
        public Guid RoomId { get; set; }

        //Navigation propertiesz    
        public Room Room { get; set; }
    }
}
