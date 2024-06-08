using Microsoft.AspNetCore.Identity;

namespace Hotel_Booking.Models.DTO
{
    public class UserRolesViewDto
    {
        public IdentityUser User { get; set; }
        public List<string> Roles { get; set; }
    }
}
