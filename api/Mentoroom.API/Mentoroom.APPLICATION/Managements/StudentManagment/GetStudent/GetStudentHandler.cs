using MediatR;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;
using Mentoroom.DOMAIN.Models;
using Microsoft.AspNetCore.Identity;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.GetAllStudents
{
    public class GetStudentHandler(IStudentRepository studentRepository, UserManager<AppUser> userManager) : IRequestHandler<GetStudent, IEnumerable<StudentModel>>
    {
        private readonly IStudentRepository studentRepository = studentRepository;
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task<IEnumerable<StudentModel>> Handle(GetStudent request, CancellationToken cancellationToken)
        {
            var students = await studentRepository.GetAllStudents();
            var users = await userManager.GetUsersInRoleAsync(UserRoles.Student);
            var studentDtos = new List<StudentModel>();

            if (students.Any() != true || users.Any() != true)
            {
                throw new Exception("There are no students in the database");
            }
            foreach (var user in users)
            {
                studentDtos.Add(new StudentModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = UserRoles.Student,
                    IndexNumber = students.FirstOrDefault(s => s.UserId == user.Id).IndexNumber,
                });
            }
            return studentDtos;
        }
    }
}
