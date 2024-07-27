using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Tags.Queries.Major.GetMajor
{
    public class GetMajor : IRequest<IEnumerable<MajorModel>>
    {
    }
}
