using Hotel_Booking.Models.Domains;
using Hotel_Booking.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hotel_Booking.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingsController(IBookingRepository bookingRepository)
        {
            this._bookingRepository = bookingRepository;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _bookingRepository.GetAllAsync();

            return View(bookings);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid RoomId)
        {
            var isLogin = HttpContext.Session.GetString("jwtToken");
            if (isLogin == null)
            {
                return Unauthorized();
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            string userId = userIdClaim.Value;

            Booking newBooking = new Booking();
            newBooking.Id = Guid.NewGuid();
            newBooking.BookingDate = DateTime.Now;
            newBooking.RoomId = RoomId;
            newBooking.CustomerId = HttpContext.Session.GetString("CurrentUserId").ToString();
            newBooking.CheckingDate = DateTime.Now;
            newBooking.CheckOutDate = DateTime.Now;
            newBooking.Status = "Booked";

            var booking = await _bookingRepository.AddNewAsync(newBooking);

            if (booking is not null)
            {
                return RedirectToAction("Index", "Bookings");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            var booking = await _bookingRepository.GetByIdAsync(id);

            if (booking is not null)
            {
                return View(booking);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Booking request)
        {
            var booking = await _bookingRepository.AddNewAsync(request);

            if (booking is not null)
            {
                return RedirectToAction("Index", "Bookings");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Booking request)
        {

            var booking = _bookingRepository.DeleteByIdAsync(request.Id);

            if (booking is not null)
            {
                return RedirectToAction("Index", "Bookings");

            }

            return View("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Cancel()
        {
            var userId = HttpContext.Session.GetString("CurrentUserId").ToString();

            if (String.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var isDelete = await _bookingRepository.DeleteRangeByUserIdAsync(userId);

            if (isDelete == true)
            {
                return RedirectToAction("Index", "Home");

            }

            return View("Index");
        }
    }
}
