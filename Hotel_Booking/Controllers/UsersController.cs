using Hotel_Booking.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllAsync();

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is not null)
            {
                return View(user);
            }

            return View(null);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(IdentityUser newUser)
        {

            var user = await _userRepository.AddNewAsync(newUser);

            if (user is not null)
            {
                return RedirectToAction("Index", "Users");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {

            var user = await _userRepository.GetByIdAsync(id);

            if (user is not null)
            {
                return View(user);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IdentityUser request)
        {
            var user = await _userRepository.AddNewAsync(request);

            if (user is not null)
            {
                return RedirectToAction("Index", "Users");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(IdentityUser request)
        {

            var user = _userRepository.DeleteByIdAsync(request.Id);

            if (user is not null)
            {
                return RedirectToAction("Index", "Users");

            }

            return View("Edit");
        }
    }
}
