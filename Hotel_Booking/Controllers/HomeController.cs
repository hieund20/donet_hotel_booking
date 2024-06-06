using Hotel_Booking.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRoomRepository _roomRepository;

        public HomeController(ILogger<HomeController> logger, IRoomRepository roomRepository)
        {
            this._logger = logger;
            this._roomRepository = roomRepository;
        }

        public async Task<IActionResult> Index()
        {
            var rooms = await _roomRepository.GetAllAsync();

            return View(rooms);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
