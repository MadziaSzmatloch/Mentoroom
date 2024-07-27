using MediatR;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Commands.ConfirmStudentCourse
{
    public class ConfirmStudentCourse : IRequest
    {
        public Guid courseId { get; set; }
        public string studentId { get; set; }
    }
}
