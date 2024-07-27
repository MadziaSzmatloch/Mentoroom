using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Entities.Shared;
using Riok.Mapperly.Abstractions;

namespace Mentoroom.APPLICATION.Mappings
{
    [Mapper]
    public partial class AppUserMapper
    {
        public UserModel AppUserToUserDto(AppUser appUser)
        {
            if (appUser == null)
            {
                throw new Exception("User does not exist");
            }
            else
            {
                var userDto = new UserModel()
                {
                    Id = appUser.Id,
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    Email = appUser.Email,
                };
                return userDto;
            }
        }
    }
}
