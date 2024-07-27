using MediatR;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Commands.AddStudentCourse
{
    public class AddStudentCourse : IRequest
    {
        public string StudentId { get; set; }
        public Guid CourseId { get; set; }
    }
}
