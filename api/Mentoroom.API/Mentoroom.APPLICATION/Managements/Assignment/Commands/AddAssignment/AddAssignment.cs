using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Assignment.Commands.AddAssignment
{
    public class AddAssignment : IRequest<AssignmentDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DeadlineDate { get; set; }
        public bool isActive { get; set; }
        public Guid CourseId { get; set; }
        public List<FileModel> Files { get; set; }
    }
}
