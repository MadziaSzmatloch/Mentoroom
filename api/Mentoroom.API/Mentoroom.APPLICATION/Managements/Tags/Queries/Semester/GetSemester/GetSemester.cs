using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Tags.Queries.Semester.GetSemester
{
    public class GetSemester : IRequest<IEnumerable<SemesterModel>>
    {
    }
}
