using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Interfaces.Tags;

namespace Mentoroom.APPLICATION.Managements.Tags.Commands.AddMajor
{
    public class AddMajorHandler(IMajorRepository majorRepository) : IRequestHandler<AddMajor, MajorModel>
    {
        public async Task<MajorModel> Handle(AddMajor request, CancellationToken cancellationToken)
        {
            var major = await majorRepository.AddMajor(new DOMAIN.Entities.Tags.Major()
            {
                Name = request.Name,
                DepartmentId = request.DepartmentId,
            });
            var mapper = new TagsMapper();
            return mapper.MajorToMajorModel(major);
        }
    }
}
