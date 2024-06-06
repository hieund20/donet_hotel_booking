using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking.Controllers
{
    public class PaymentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
