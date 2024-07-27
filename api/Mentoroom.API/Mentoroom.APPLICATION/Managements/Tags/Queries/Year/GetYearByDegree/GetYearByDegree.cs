using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Tags.Queries.Year.GetYearByDegree
{
    public class GetYearByDegree : IRequest<IEnumerable<YearModel>>
    {
        public Guid DegreeId { get; set; }
    }
}
