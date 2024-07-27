using MediatR;

namespace Mentoroom.APPLICATION.Managements.Assignment.Commands.DeleteAssignment
{
    public class DeleteAssignment : IRequest
    {
        public Guid AssignmentId { get; set; }
    }
}
