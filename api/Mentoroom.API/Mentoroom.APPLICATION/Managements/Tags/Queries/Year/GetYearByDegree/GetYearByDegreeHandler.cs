using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Interfaces.Tags;

namespace Mentoroom.APPLICATION.Managements.Tags.Queries.Year.GetYearByDegree
{
    public class GetYearByDegreeHandler(IYearRepository yearRepository, IDegreeRepository degreeRepository) : IRequestHandler<GetYearByDegree, IEnumerable<YearModel>>
    {
        public async Task<IEnumerable<YearModel>> Handle(GetYearByDegree request, CancellationToken cancellationToken)
        {
            var degree = await degreeRepository.GetDegreeById(request.DegreeId);
            if (degree == null)
            {
                throw new Exception("This degree does not exist");
            }
            var mapper = new TagsMapper();
            var years = await yearRepository.GetYearsByDegree(request.DegreeId);
            if (years.Any() == false)
            {
                throw new Exception("There are no years in the database");
            }
            var yearModels = years.Select(x => mapper.YearToYearModel(x));
            return yearModels;
        }
    }
}
