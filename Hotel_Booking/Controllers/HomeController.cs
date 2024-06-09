using Hotel_Booking.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHotelRepository _hotelRepository;

        public HomeController(ILogger<HomeController> logger, IHotelRepository hotelRepository)
        {
            this._logger = logger;
            this._hotelRepository = hotelRepository;
        }

        public async Task<IActionResult> Index()
        {
            var hotels = await _hotelRepository.GetAllAsync();

            return View(hotels);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
