using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Hotel_Booking.Models.DTO;
using Hotel_Booking.Repositories;
using System.Net.Http;
using System.Text;


namespace Hotel_Booking.Controllers
{
    public class AuthController : Controller
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;


        public AuthController(ITokenRepository tokenRepository,
                            IUserRepository userRepository,
                            UserManager<IdentityUser> userManager)
        {
            this._tokenRepository = tokenRepository;
            this._userRepository = userRepository;
            this._userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Username);
            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

                if (checkPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        var jwtToken = _tokenRepository.CreateJWTToken(user, roles.ToList());

                        var userLogin = await _userRepository.GetByJwtToken(jwtToken);

                        if (userLogin != null)
                        {
                            HttpContext.Session.SetString("jwtToken", jwtToken.ToString());
                            HttpContext.Session.SetString("CurrentUserName", userLogin.UserName.ToString());
                            HttpContext.Session.SetString("CurrentUserEmail", userLogin.Email.ToString());
                            HttpContext.Session.SetString("CurrentUserId", userLogin.Id.ToString());
                            HttpContext.Session.SetString("CurrentUserRoles", JsonSerializer.Serialize(roles));
                        }
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View("Login Faild");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequest)
        {
            if (registerRequest.Password != registerRequest.ConfirmPassword)
            {
                return View("Mật khẩu và xác nhận mật khẩu phải trùng nhau");
            }    

            var indentityUser = new IdentityUser
            {
                UserName = registerRequest.Username,
                Email = registerRequest.Username
            };

            var identityResult = await _userManager.CreateAsync(indentityUser, registerRequest.Password);

            if (identityResult.Succeeded)
            {
                List<string> roles = new List<string>();
                roles.Add("Reader");

                identityResult = await _userManager.AddToRolesAsync(indentityUser, roles);

                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Login", "Auth");
                }
            }

            return View(identityResult.Errors);
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile()
        {
            var jwtToken = HttpContext.Session.GetString("jwtToken");

            var userLogin = await _userRepository.GetByJwtToken(jwtToken.ToString());

            if (userLogin is not null)
            {
                return View(userLogin);
            }

            return View("Không thể lấy thông tin người dùng");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}
