using Hotel_Booking.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Hotel_Booking.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Booking.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(IUserRepository userRepository, 
                            UserManager<IdentityUser> userManager,
                            RoleManager<IdentityRole> roleManager)
        {
            this._userRepository = userRepository;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {

            var users = await _userRepository.GetAllAsync();
            var userRolesViewModel = new List<UserRolesViewDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRolesViewModel.Add(new UserRolesViewDto
                {
                    User = user,
                    Roles = (List<string>)roles
                });
            }

            return View(userRolesViewModel);
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
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

            var model = new EditUserRolesViewDto
            {
                User = user,
                UserRoles = userRoles,
                AllRoles = allRoles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserRolesViewDto request)
        {
            var user = await _userManager.FindByIdAsync(request.User.Id);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var selectedRoles = request.SelectedRoles ?? new List<string>();

            var addRoles = selectedRoles.Except(userRoles);
            var removeRoles = userRoles.Except(selectedRoles);

            await _userManager.AddToRolesAsync(user, addRoles);
            await _userManager.RemoveFromRolesAsync(user, removeRoles);

            return RedirectToAction("Index");
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
