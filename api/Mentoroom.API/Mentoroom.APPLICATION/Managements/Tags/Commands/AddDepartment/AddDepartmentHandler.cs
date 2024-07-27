using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Interfaces.Tags;

namespace Mentoroom.APPLICATION.Managements.Tags.Commands.AddDepartment
{
    public class AddDepartmentHandler(IDepartmentRepository departmentRepository) : IRequestHandler<AddDepartment, DepartmentModel>
    {
        public async Task<DepartmentModel> Handle(AddDepartment request, CancellationToken cancellationToken)
        {
            var department = await departmentRepository.AddDepartment(new DOMAIN.Entities.Tags.Department()
            {
                Name = request.Name,
                ShortName = request.ShortName
            });
            var mapper = new TagsMapper();
            return mapper.DepartmentToDepartmentModel(department);
        }
    }
}
