using Hotel_Booking.Models.Domains;
using Microsoft.AspNetCore.Identity;

namespace Hotel_Booking.Repositories
{
    public interface IUserRepository
    {
        Task<IdentityUser> GetByJwtToken(string jwtToken);
        Task<List<string>> GetRoles(string jwtToken);

        Task<List<IdentityUser>> GetAllAsync();
        Task<IdentityUser?> GetByIdAsync(string id);
        Task<IdentityUser> AddNewAsync(IdentityUser room);
        Task<IdentityUser?> UpdateyIdAsync(string id, IdentityUser room);
        Task<IdentityUser?> DeleteByIdAsync(string id);
    }
}
