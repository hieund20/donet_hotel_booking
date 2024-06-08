using Microsoft.AspNetCore.Identity;

namespace Hotel_Booking.Repositories
{
    public interface IUserRepository
    {
        Task<IdentityUser> GetByJwtToken(string jwtToken);
        Task<List<string>> GetRoles(string jwtToken);
    }
}
