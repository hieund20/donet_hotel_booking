using Hotel_Booking.Models.DTO;
using Hotel_Booking.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace Hotel_Booking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHotelRepository _hotelRepository;
        private readonly IHotelImageRepository _hotelImageRepository;

        public HomeController(ILogger<HomeController> logger, IHotelRepository hotelRepository, IHotelImageRepository hotelImageRepository)
        {
            this._logger = logger;
            this._hotelRepository = hotelRepository;
            this._hotelImageRepository = hotelImageRepository;
        }

        public async Task<IActionResult> Index()
        {
            var hotels = await _hotelRepository.GetAllAsync();

            List<HotelWithImageViewDto> result = new List<HotelWithImageViewDto>();

            foreach (var item in hotels)
            {
                var image = await _hotelImageRepository.GetByHotelIdAsync(item.Id);
                if (image != null)
                {
                    result.Add(new HotelWithImageViewDto
                    {
                        Hotel = item,
                        ImageUrl = image.FilePath
                    });
                }
            }

            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
