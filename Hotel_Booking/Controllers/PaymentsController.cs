using Hotel_Booking.Models.Domains;
using Hotel_Booking.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentsController(IPaymentRepository paymentRepository)
        {
            this._paymentRepository = paymentRepository;
        }

        public async Task<IActionResult> Index()
        {
            var payments = await _paymentRepository.GetAllAsync();

            return View(payments);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Payment newPayment)
        {
            var payment = await _paymentRepository.AddNewAsync(newPayment);

            if (payment is not null)
            {
                return RedirectToAction("Index", "Payments");
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Confirm()
        {
            var isLogin = HttpContext.Session.GetString("jwtToken");
            if (isLogin == null)
            {
                return Unauthorized();
            }

            string userId = HttpContext.Session.GetString("CurrentUserId").ToString();
            var payment = await _paymentRepository.ConfirmAsync(userId);

            if (!payment)
            {
                return RedirectToAction("Index", "Payments");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Payment request)
        {

            var payment = _paymentRepository.DeleteByIdAsync(request.Id);

            if (payment is not null)
            {
                return RedirectToAction("Index", "Payments");
            }

            return View("Edit");
        }
    }
}
