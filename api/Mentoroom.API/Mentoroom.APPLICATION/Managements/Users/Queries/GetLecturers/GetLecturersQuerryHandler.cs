using MediatR;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.APPLICATION.Managements.Users.Queries.GetLecturers
{
    internal class GetLecturersQuerryHandler(UserManager<AppUser> userManager) : IRequestHandler<GetLecturersQuerry, List<UserModel>>
    {
        public async Task<List<UserModel>> Handle(GetLecturersQuerry request, CancellationToken cancellationToken)
        {
            var users = await userManager.Users.ToListAsync();
            List<UserModel> result = new List<UserModel>();
            foreach (var user in users)
            {
                var role = await GetUserRole(user);
                var userDto = new UserModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email!,
                    Role = role
                };

                result.Add(userDto);
            }

            return result.Where(x => x.Role != null && x.Role.Equals(UserRoles.Lecturer, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }
        private async Task<string?> GetUserRole(AppUser user)
        {
            var roles = await userManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }
    }
}
