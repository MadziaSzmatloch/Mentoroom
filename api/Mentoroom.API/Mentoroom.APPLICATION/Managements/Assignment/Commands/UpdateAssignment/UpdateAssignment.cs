using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Assignment.Commands.UpdateAssignment
{
    public class UpdateAssignment : IRequest<AssignmentDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DeadlineDate { get; set; }
        public bool isActive { get; set; }
    }
}
