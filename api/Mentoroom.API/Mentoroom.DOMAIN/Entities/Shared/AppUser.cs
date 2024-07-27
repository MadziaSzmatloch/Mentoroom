using Microsoft.AspNetCore.Identity;

namespace Mentoroom.DOMAIN.Entities.Shared
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
