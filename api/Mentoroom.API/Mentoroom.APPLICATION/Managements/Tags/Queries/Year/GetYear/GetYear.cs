using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Tags.Queries.Year.GetYear
{
    public class GetYear : IRequest<IEnumerable<YearModel>>
    {
    }
}
