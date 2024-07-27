using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Tags.Queries.Major.GetMajorByDepartment
{
    public class GetMajorByDepartment : IRequest<IEnumerable<MajorModel>>
    {
        public Guid DepartmentId { get; set; }
    }
}
