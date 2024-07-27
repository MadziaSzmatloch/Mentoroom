using MediatR;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Commands.RemoveStudentCourse
{
    public class RemoveStudentCourse : IRequest
    {
        public Guid CourseId { get; set; }
        public string StudentId { get; set; }
    }
}
