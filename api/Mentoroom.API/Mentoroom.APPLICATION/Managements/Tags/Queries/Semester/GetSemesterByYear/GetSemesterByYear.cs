using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Tags.Queries.Semester.GetSemesterByYear
{
    public class GetSemesterByYear : IRequest<IEnumerable<SemesterModel>>
    {
        public Guid YearId { get; set; }
    }
}
