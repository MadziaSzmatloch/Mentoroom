using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Interfaces.Tags;

namespace Mentoroom.APPLICATION.Managements.Tags.Queries.Degree
{
    public class GetDegreeHandler(IDegreeRepository degreeRepository) : IRequestHandler<GetDegree, IEnumerable<DegreeModel>>
    {
        public async Task<IEnumerable<DegreeModel>> Handle(GetDegree request, CancellationToken cancellationToken)
        {
            var mapper = new TagsMapper();
            var degrees = await degreeRepository.GetAllDegree();
            if (degrees.Any() == false)
            {
                throw new Exception("There are no degrees in the database");
            }
            var degreeModels = degrees.Select(x => mapper.DegreeToDegreeModel(x));
            return degreeModels;
        }
    }
}
