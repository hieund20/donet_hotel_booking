using Hotel_Booking.Models.Domains;
using Hotel_Booking.Models.DTO;
using Hotel_Booking.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking.Controllers
{
    public class HotelsController : Controller
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IHotelImageRepository _hotelImageRepository;

        public HotelsController(IHotelRepository hotelRepository, IHotelImageRepository hotelImageRepository)
        {
            this._hotelRepository = hotelRepository;
            this._hotelImageRepository = hotelImageRepository;
        }

        public async Task<IActionResult> Index()
        {
            var hotels = await _hotelRepository.GetAllAsync();

            return View(hotels);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var hotel = await _hotelRepository.GetByIdAsync(id);

            if (hotel is not null)
            {
                var image = await _hotelImageRepository.GetByHotelIdAsync(id);

                HotelWithImageViewDto hotelWithImage = new HotelWithImageViewDto();
                hotelWithImage.Hotel = hotel;
                hotelWithImage.ImageUrl = image.FilePath;

                return View(hotelWithImage);
            }

            return View(null);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Hotel newHotel)
        {

            var hotel = await _hotelRepository.AddNewAsync(newHotel);

            if (hotel is not null)
            {
                return RedirectToAction("Index", "Hotels");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            var hotel = await _hotelRepository.GetByIdAsync(id);

            if (hotel is not null)
            {
                return View(hotel);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Hotel request)
        {
            var hotel = await _hotelRepository.AddNewAsync(request);

            if (hotel is not null)
            {
                return RedirectToAction("Index", "Hotels");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Hotel request)
        {

            var hotel = _hotelRepository.DeleteByIdAsync(request.Id);

            if (hotel is not null)
            {
                return RedirectToAction("Index", "Hotels");

            }

            return View("Edit");
        }
    }
}
