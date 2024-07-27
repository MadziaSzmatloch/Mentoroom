using MediatR;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Models;
using Microsoft.AspNetCore.Identity;

namespace Mentoroom.APPLICATION.Managements.Lecturer.GetLecturers
{
    internal class GetLecturesCommandHandler(UserManager<AppUser> userManager) : IRequestHandler<GetLecturesCommand, List<UserModel>>
    {
        private readonly UserManager<AppUser> userManager = userManager;
        public async Task<List<UserModel>> Handle(GetLecturesCommand request, CancellationToken cancellationToken)
        {
            var lecturers = await userManager.GetUsersInRoleAsync(UserRoles.Lecturer);

            List<UserModel> result = new List<UserModel>();
            foreach (var user in lecturers)
            {
                var userDto = new UserModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email!,
                    Role = UserRoles.Lecturer,
                };

                result.Add(userDto);
            }
            return result;
        }
    }
}
