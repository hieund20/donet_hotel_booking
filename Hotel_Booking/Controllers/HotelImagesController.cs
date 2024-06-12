using Hotel_Booking.Models.Domains;
using Hotel_Booking.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking.Controllers
{
    public class HotelImagesController : Controller
    {
        private readonly IHotelImageRepository _hotelImageRepository;

        public HotelImagesController(IHotelImageRepository hotelImageRepository)
        {
            this._hotelImageRepository = hotelImageRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetAllImageByHotelId/{hotelId}")]
        public async Task<IActionResult> GetAllByHotelId([FromRoute] Guid hotelId)
        {
            ViewBag.HotelId = hotelId;
            var images = await _hotelImageRepository.GetAllByHotelIdAsync(hotelId);

            return View("AdminHotelImagesView", images);
        }

        [HttpGet]
        public IActionResult Add([FromQuery]Guid hotelId)
        {
            ViewBag.HotelId = hotelId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(HotelImage request)
        {
            HotelImage image = new HotelImage();
            image.Id = Guid.NewGuid();
            image.FileName = request.FileName;
            image.FileDesciption = request.FileDesciption;
            image.HotelId = request.HotelId;
            image.File = request.File;
            image.FileExtension = Path.GetExtension(request.File.FileName);
            image.FileSizeInBytes = request.File.Length;

            var result = await _hotelImageRepository.Upload(image);

            if (result is not null)
            {
                var images = await _hotelImageRepository.GetAllByHotelIdAsync(request.HotelId);

                return View("AdminHotelImagesView", images);
            }

            return View();
        }
    }
}
