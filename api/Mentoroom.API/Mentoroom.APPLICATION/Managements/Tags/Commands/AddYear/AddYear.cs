using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Tags.Commands.AddYear
{
    public class AddYear : IRequest<YearModel>
    {
        public string Name { get; set; }
        public Guid DegreeId { get; set; }
    }
}
