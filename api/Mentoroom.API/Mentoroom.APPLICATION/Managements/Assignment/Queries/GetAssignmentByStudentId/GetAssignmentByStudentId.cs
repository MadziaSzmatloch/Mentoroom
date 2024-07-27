using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Assignment.Queries.GetAssignmentByStudentId
{
    public class GetAssignmentByStudentId : IRequest<List<AssignmentDto>>
    {
        public string StudentId { get; set; }
    }
}
