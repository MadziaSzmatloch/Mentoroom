using MediatR;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Commands.DeleteStudentCourse
{
    public class DeleteStudentCourse : IRequest
    {
        public string StudentId { get; set; }
        public Guid CourseId { get; set; }
    }
}
