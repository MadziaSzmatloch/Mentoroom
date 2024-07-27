using MediatR;
using Mentoroom.APPLICATION.Models;
using Mentoroom.APPLICATION.Services.AuthServices;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Models;
using Microsoft.AspNetCore.Identity;

namespace Mentoroom.APPLICATION.Managements.Auth.Promote.PromoteToLecturrer
{
    public class PromoteToLecturer : IRequest<UserModel>
    {
        public string UserID { get; set; }
    }

    public class PromoteToLecturerHandler(IAuthService authService, UserManager<AppUser> userManager) : IRequestHandler<PromoteToLecturer, UserModel>
    {
        private readonly IAuthService authService = authService;
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task<UserModel> Handle(PromoteToLecturer request, CancellationToken cancellationToken)
        {
            var result = await authService.PromoteTo(UserRoles.Lecturer, request.UserID);
            if (!result)
            {
                throw new Exception("Something went wrong!");
            }
            else
            {
                var user = await userManager.FindByIdAsync(request.UserID);
                var role = await GetUserRole(user);
                var userDto = new UserModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email!,
                    Role = role
                };
                return userDto;
            }
        }

        private async Task<string?> GetUserRole(AppUser user)
        {
            var roles = await userManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }
    }
}
