using Microsoft.AspNetCore.Identity;

namespace Hotel_Booking.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
