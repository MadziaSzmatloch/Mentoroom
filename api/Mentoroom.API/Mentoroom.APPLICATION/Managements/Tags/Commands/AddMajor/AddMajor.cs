using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Tags.Commands.AddMajor
{
    public class AddMajor : IRequest<MajorModel>
    {
        public string Name { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
