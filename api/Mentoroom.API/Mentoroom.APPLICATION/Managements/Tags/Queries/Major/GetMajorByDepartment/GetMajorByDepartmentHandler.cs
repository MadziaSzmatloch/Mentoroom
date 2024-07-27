using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Interfaces.Tags;

namespace Mentoroom.APPLICATION.Managements.Tags.Queries.Major.GetMajorByDepartment
{
    public class GetMajorByDepartmentHandler(IMajorRepository majorRepository, IDepartmentRepository departmentRepository) : IRequestHandler<GetMajorByDepartment, IEnumerable<MajorModel>>
    {
        public async Task<IEnumerable<MajorModel>> Handle(GetMajorByDepartment request, CancellationToken cancellationToken)
        {
            var department = await departmentRepository.GetDepartmentById(request.DepartmentId);
            if (department == null)
            {
                throw new Exception("This department does not exist");
            }
            var mapper = new TagsMapper();
            var majors = await majorRepository.GetMajorsByDepartment(request.DepartmentId);
            if (majors.Any() == false)
            {
                throw new Exception("There are no majors in the database");
            }
            var majorModel = majors.Select(x => mapper.MajorToMajorModel(x));
            return majorModel;
        }
    }
}
