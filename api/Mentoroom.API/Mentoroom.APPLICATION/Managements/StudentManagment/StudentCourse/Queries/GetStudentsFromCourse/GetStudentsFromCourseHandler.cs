using MediatR;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;
using Mentoroom.DOMAIN.Models;
using Microsoft.AspNetCore.Identity;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Queries.GetStudentsFromCourse
{
    public class GetStudentsFromCourseHandler(
        IStudentCourseRepository studentCourseRepository,
        UserManager<AppUser> userManager,
        ICourseRepository courseRepository) : IRequestHandler<GetStudentsFromCourse, List<GetStudentModel>>
    {
        private readonly IStudentCourseRepository studentCourseRepository = studentCourseRepository;
        private readonly UserManager<AppUser> userManager = userManager;
        private readonly ICourseRepository courseRepository = courseRepository;

        public async Task<List<GetStudentModel>> Handle(GetStudentsFromCourse request, CancellationToken cancellationToken)
        {
            var course = await courseRepository.GetCourseById(request.CourseId);
            if (course == null)
            {
                throw new Exception("This course does not exist");
            }

            var studentCourses = await studentCourseRepository.GetStudentCoursesByCourseId(request.CourseId);

            if (studentCourses.Count() == 0)
            {
                throw new Exception("This course does not have any participiants");
            }

            List<GetStudentModel> students = new List<GetStudentModel>();
            foreach (var studentCourse in studentCourses)
            {
                var student = await userManager.FindByIdAsync(studentCourse.StudentId);
                students.Add(new GetStudentModel()
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Email = student.Email,
                    Role = UserRoles.Student,
                    IsConfirmed = studentCourse.IsConfirmed,
                });
            }
            return students;
        }
    }
}
