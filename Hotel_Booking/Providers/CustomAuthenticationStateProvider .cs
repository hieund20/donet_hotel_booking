using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Hotel_Booking.Providers
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            var userEmail = _httpContextAccessor.HttpContext.Session.GetString("UserEmail");
            var userRoles = _httpContextAccessor.HttpContext.Session.GetString("UserRoles");


            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(userEmail) && !string.IsNullOrEmpty(userRoles))
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Email, userEmail)
                }, "apiauth_type");

                var roles = userRoles.Split(',');

                foreach (var role in roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }

                var user = new ClaimsPrincipal(identity);
                return Task.FromResult(new AuthenticationState(user));
            }
            else
            {
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
            }
        }
    }
}
