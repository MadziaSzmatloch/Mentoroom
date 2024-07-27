using MediatR;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;
using Microsoft.AspNetCore.Identity;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Commands.AddStudentCourse
{
    public class AddStudentCourseHandler(
        IStudentCourseRepository studentCourseRepository,
        IStudentRepository studentRepository,
        ICourseRepository courseRepository,
        UserManager<AppUser> userManager) : IRequestHandler<AddStudentCourse>
    {
        private readonly IStudentCourseRepository studentCourseRepository = studentCourseRepository;
        private readonly ICourseRepository courseRepository = courseRepository;
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task Handle(AddStudentCourse request, CancellationToken cancellationToken)
        {
            var student = await userManager.FindByIdAsync(request.StudentId);
            var course = await courseRepository.GetCourseById(request.CourseId);
            var sCourse = (await studentCourseRepository.GetAllStudentCourses()).FirstOrDefault(sc => sc.CourseId == request.CourseId && sc.StudentId == request.StudentId);
            if (student == null)
            {
                throw new Exception("This student does not exist");
            }
            else if (course == null)
            {
                throw new Exception("This course does not exist");
            }
            else if (sCourse != null)
            {
                if (sCourse.IsConfirmed == true)
                {
                    throw new Exception("This student is already in this course");
                }
                else if (sCourse.IsConfirmed == false)
                {
                    throw new Exception("This student cannot signup for this course");
                }
                else
                {
                    throw new Exception("This student is waiting for admission to this course");
                }
            }
            var studentCourse = new DOMAIN.Entities.StudentModels.StudentCourse()
            {
                JoiningDate = DateTime.Now,
                StudentId = request.StudentId,
                Student = await studentRepository.GetStudentById(request.StudentId),
                CourseId = request.CourseId,
                Course = await courseRepository.GetCourseById(request.CourseId),
            };

            studentCourse = await studentCourseRepository.AddStudentCourse(studentCourse);
        }
    }
}
