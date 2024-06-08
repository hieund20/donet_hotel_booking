using Microsoft.AspNetCore.Identity;

namespace Hotel_Booking.Models.DTO
{
    public class EditUserRolesViewDto
    {
        public IdentityUser User { get; set; }
        public IList<string> UserRoles { get; set; }
        public IList<string> AllRoles { get; set; }
        public List<string> SelectedRoles { get; set; }
    }
}
