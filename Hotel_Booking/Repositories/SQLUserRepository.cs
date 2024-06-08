using Hotel_Booking.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Hotel_Booking.Repositories
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly HotelBookingAuthDBContext _dBAuthContext;

        public SQLUserRepository(UserManager<IdentityUser> userManager, HotelBookingAuthDBContext dBAuthContext)
        {
            this.userManager = userManager;
            this._dBAuthContext = dBAuthContext;
        }

        public async Task<IdentityUser> AddNewAsync(IdentityUser user)
        {
            await _dBAuthContext.Users.AddAsync(user);
            await _dBAuthContext.SaveChangesAsync();
            return user;
        }

        public async Task<IdentityUser?> DeleteByIdAsync(string id)
        {
            var existingUser = await _dBAuthContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (existingUser == null)
            {
                return null;
            }

            _dBAuthContext.Users.Remove(existingUser);
            await _dBAuthContext.SaveChangesAsync();
            return existingUser;
        }

        public async Task<List<IdentityUser>> GetAllAsync()
        {
            var users = await _dBAuthContext.Users.ToListAsync();
            return users;
        }

        public async Task<IdentityUser?> GetByIdAsync(string id)
        {
            var user = await _dBAuthContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<IdentityUser?> UpdateyIdAsync(string id, IdentityUser user)
        {

            var existingUser = await _dBAuthContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.PhoneNumber = user.PhoneNumber;

            await _dBAuthContext.SaveChangesAsync();

            return existingUser;
        }

        public async Task<IdentityUser> GetByJwtToken(string jwtToken)
        {
            // Validate and decode the JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(jwtToken);

            // Check if the 'sub' claim exists
            var userEmailClaim = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (userEmailClaim == null)
            {
                return null;
            }

            // Retrieve user ID from the 'sub' claim
            var userEmail = userEmailClaim.Value;

            var user = await userManager.FindByEmailAsync(userEmail);

            if (user is null)
            {
                return null;
            }

            return user;
        }

        public async Task<List<string>> GetRoles(string jwtToken)
        {
            var currentUser = await GetByJwtToken(jwtToken);

            if (currentUser is null)
            {
                return null;
            }

            List<string> roles = new List<string>();
            roles.AddRange(await userManager.GetRolesAsync(currentUser));

            if (roles is null)
            {
                return null;
            }

            return roles;
        } 
    }
}
