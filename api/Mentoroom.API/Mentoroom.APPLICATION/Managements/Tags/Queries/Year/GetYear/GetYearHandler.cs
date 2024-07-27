using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Interfaces.Tags;

namespace Mentoroom.APPLICATION.Managements.Tags.Queries.Year.GetYear
{
    public class GetYearHandler(IYearRepository yearRepository) : IRequestHandler<GetYear, IEnumerable<YearModel>>
    {
        public async Task<IEnumerable<YearModel>> Handle(GetYear request, CancellationToken cancellationToken)
        {
            var mapper = new TagsMapper();
            var years = await yearRepository.GetAllYear();
            if (years.Any() == false)
            {
                throw new Exception("There are no years in the database");
            }
            var yearModels = years.Select(x => mapper.YearToYearModel(x));
            return yearModels;
        }
    }
}
