using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Interfaces.Tags;

namespace Mentoroom.APPLICATION.Managements.Tags.Commands.AddDegree
{
    public class AddDegreeHandler(IDegreeRepository degreeRepository) : IRequestHandler<AddDegree, DegreeModel>
    {
        public async Task<DegreeModel> Handle(AddDegree request, CancellationToken cancellationToken)
        {
            var degree = await degreeRepository.AddDegree(new DOMAIN.Entities.Tags.Degree()
            {
                Name = request.Name,
            });
            var mapper = new TagsMapper();
            return mapper.DegreeToDegreeModel(degree);
        }
    }
}
