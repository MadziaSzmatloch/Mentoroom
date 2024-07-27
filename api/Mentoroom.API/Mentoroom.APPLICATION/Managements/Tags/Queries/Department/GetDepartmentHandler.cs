using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Interfaces.Tags;

namespace Mentoroom.APPLICATION.Managements.Tags.Queries.Department
{
    public class GetDepartmentHandler(IDepartmentRepository departmentRepository) : IRequestHandler<GetDepartment, IEnumerable<DepartmentModel>>
    {
        public async Task<IEnumerable<DepartmentModel>> Handle(GetDepartment request, CancellationToken cancellationToken)
        {
            var mapper = new TagsMapper();
            var departments = await departmentRepository.GetAllDepartments();
            if (departments.Any() == false)
            {
                throw new Exception("There are no departments in the database");
            }
            var departmentModels = departments.Select(x => mapper.DepartmentToDepartmentModel(x));
            return departmentModels;
        }
    }
}
