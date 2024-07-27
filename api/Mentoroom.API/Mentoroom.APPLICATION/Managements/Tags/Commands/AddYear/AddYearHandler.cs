using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Interfaces.Tags;

namespace Mentoroom.APPLICATION.Managements.Tags.Commands.AddYear
{
    public class AddYearHandler(IYearRepository yearRepository) : IRequestHandler<AddYear, YearModel>
    {
        public async Task<YearModel> Handle(AddYear request, CancellationToken cancellationToken)
        {
            var year = await yearRepository.AddYear(new DOMAIN.Entities.Tags.Year()
            {
                Name = request.Name,
                DegreeId = request.DegreeId,
            });
            var mapper = new TagsMapper();
            return mapper.YearToYearModel(year);
        }
    }
}
