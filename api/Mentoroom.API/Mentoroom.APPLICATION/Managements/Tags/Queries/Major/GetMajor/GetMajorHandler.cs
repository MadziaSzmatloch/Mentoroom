using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Interfaces.Tags;

namespace Mentoroom.APPLICATION.Managements.Tags.Queries.Major.GetMajor
{
    public class GetMajorHandler(IMajorRepository majorRepository) : IRequestHandler<GetMajor, IEnumerable<MajorModel>>
    {
        public async Task<IEnumerable<MajorModel>> Handle(GetMajor request, CancellationToken cancellationToken)
        {
            var mapper = new TagsMapper();
            var majors = await majorRepository.GetAllMajors();
            if (majors.Any() == false)
            {
                throw new Exception("There are no majors in the database");
            }
            var majorModel = majors.Select(x => mapper.MajorToMajorModel(x));
            return majorModel;
        }
    }
}
