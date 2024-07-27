using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Tags.Queries.Degree
{
    public class GetDegree : IRequest<IEnumerable<DegreeModel>>
    {
    }
}
