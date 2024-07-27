using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Tags.Commands.AddDegree
{
    public class AddDegree : IRequest<DegreeModel>
    {
        public string Name { get; set; }
    }
}
