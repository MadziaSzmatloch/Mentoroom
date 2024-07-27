using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Tags.Queries.Department
{
    public class GetDepartment : IRequest<IEnumerable<DepartmentModel>>
    {
    }
}
