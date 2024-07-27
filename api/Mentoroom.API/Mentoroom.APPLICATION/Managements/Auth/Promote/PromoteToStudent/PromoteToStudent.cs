using MediatR;
using Mentoroom.APPLICATION.Models;
using Mentoroom.APPLICATION.Services.AuthServices;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Entities.StudentModels;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;
using Mentoroom.DOMAIN.Models;
using Microsoft.AspNetCore.Identity;

namespace Mentoroom.APPLICATION.Managements.Auth.Promote.PromoteToStudent
{
    public class PromoteToStudent : IRequest<UserModel>
    {
        public string UserID { get; set; }
        public string StudentIndex { get; set; }
    }

    public class PromoteToStudentHandler(IAuthService authService, IStudentRepository studentRepository, UserManager<AppUser> userManager) : IRequestHandler<PromoteToStudent, UserModel>
    {
        private readonly IAuthService authService = authService;
        private readonly IStudentRepository studentRepository = studentRepository;
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task<UserModel> Handle(PromoteToStudent request, CancellationToken cancellationToken)
        {
            var result = await authService.PromoteTo(UserRoles.Student, request.UserID);
            if (!result)
            {
                throw new Exception("Something went wrong!");
            }
            else
            {
                await studentRepository.AddStudent(new Student()
                {
                    UserId = request.UserID,
                    IndexNumber = request.StudentIndex
                });

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
