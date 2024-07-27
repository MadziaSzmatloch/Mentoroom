using MediatR;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;
using Microsoft.AspNetCore.Identity;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Commands.RemoveStudentCourse
{
    public class RemoveStudentCourseHandler(
        IStudentCourseRepository studentCourseRepository,
        ICourseRepository courseRepository,
        UserManager<AppUser> userManager) : IRequestHandler<RemoveStudentCourse>
    {
        private readonly IStudentCourseRepository studentCourseRepository = studentCourseRepository;
        private readonly ICourseRepository courseRepository = courseRepository;
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task Handle(RemoveStudentCourse request, CancellationToken cancellationToken)
        {
            var student = await userManager.FindByIdAsync(request.StudentId);
            var course = await courseRepository.GetCourseById(request.CourseId);
            var studentCourse = (await studentCourseRepository.GetAllStudentCourses()).FirstOrDefault(sc => sc.CourseId == request.CourseId && sc.StudentId == request.StudentId);
            if (student == null)
            {
                throw new Exception("This student does not exist");
            }
            else if (course == null)
            {
                throw new Exception("This course does not exist");
            }
            if (studentCourse == null)
            {
                throw new Exception("This student is not on this course");
            }
            else
            {
                if (studentCourse.IsConfirmed == true)  //waiting to be confirmed
                {
                    await studentCourseRepository.RemoveStudentCourse(request.StudentId, request.CourseId);
                }
                else if (studentCourse.IsConfirmed == null)
                {
                    throw new Exception("This student does not belong to this course yet");
                }
                else
                {
                    throw new Exception("This student is already removed");
                }
            }
        }
    }
}
