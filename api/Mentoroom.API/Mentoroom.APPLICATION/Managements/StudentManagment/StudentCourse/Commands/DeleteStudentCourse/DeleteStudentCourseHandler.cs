using MediatR;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Commands.DeleteStudentCourse
{
    public class DeleteStudentCourseHandler(IStudentCourseRepository studentCourseRepository) : IRequestHandler<DeleteStudentCourse>
    {
        public async Task Handle(DeleteStudentCourse request, CancellationToken cancellationToken)
        {
            var studentCourse = (await studentCourseRepository.GetAllStudentCourses()).FirstOrDefault(sc => sc.CourseId == request.CourseId && sc.StudentId == request.StudentId);
            if (studentCourse == null)
            {
                throw new Exception("This course does not exist");
            }
            if (studentCourse.IsConfirmed == true || studentCourse.IsConfirmed == false)
            {
                throw new Exception("You cannot delete this student");
            }

            await studentCourseRepository.DeleteStudentCourse(studentCourse);
        }
    }
}
