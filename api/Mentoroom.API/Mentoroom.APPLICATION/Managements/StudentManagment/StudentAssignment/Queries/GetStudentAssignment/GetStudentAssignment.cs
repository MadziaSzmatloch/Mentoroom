using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentAssignment.Queries.GetStudentAssignment
{
    public class GetStudentAssignment : IRequest<StudentAssignmentModel>
    {
        public string StudentId { get; set; }
        public Guid AssignmnetId { get; set; }
    }
}
