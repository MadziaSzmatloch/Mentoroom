using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Queries.GetStudentCourse
{
    public class GetStudentCourse : IRequest<List<StudentAssignmentModel>>
    {
        public Guid CourseId { get; set; }
        public string StudentId { get; set; }
    }
}
