using MediatR;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Entities.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.APPLICATION.Managements.Users.Queries.GetUsers
{
    internal class GetUsersQuerryHandler(UserManager<AppUser> userManager) : IRequestHandler<GetUsersQuerry, List<UserModel>>
    {
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task<List<UserModel>> Handle(GetUsersQuerry request, CancellationToken cancellationToken)
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

            return result;
        }

        private async Task<string?> GetUserRole(AppUser user)
        {
            var roles = await userManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }
    }
}
