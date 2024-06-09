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

        [HttpGet()]
        public IActionResult Add([FromRoute]Guid hotelId)
        {
            ViewBag.HotelId = hotelId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(HotelImage model)
        {
            HotelImage image = new HotelImage();
            image.Id = Guid.NewGuid();
            image.FileName = model.FileName;
            image.FileDesciption = model.FileDesciption;
            image.HotelId = model.HotelId;

            var result = await _hotelImageRepository.Upload(image);

            if (result is not null)
            {
                var images = await _hotelImageRepository.GetAllByHotelIdAsync(model.HotelId);

                return View("AdminHotelImagesView", images);
            }

            return View();
        }
    }
}
