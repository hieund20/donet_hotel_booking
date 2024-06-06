using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking.Controllers
{
    public class HotelsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
