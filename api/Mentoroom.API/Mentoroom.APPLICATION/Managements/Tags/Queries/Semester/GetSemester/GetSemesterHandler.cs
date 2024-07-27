using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Interfaces.Tags;

namespace Mentoroom.APPLICATION.Managements.Tags.Queries.Semester.GetSemester
{
    public class GetSemesterHandler(ISemesterRepository semesterRepository) : IRequestHandler<GetSemester, IEnumerable<SemesterModel>>
    {
        public async Task<IEnumerable<SemesterModel>> Handle(GetSemester request, CancellationToken cancellationToken)
        {
            var mapper = new TagsMapper();
            var semesters = await semesterRepository.GetAllSemester();
            if (semesters.Any() == false)
            {
                throw new Exception("There are no semesters in the database");
            }
            var semesterModels = semesters.Select(x => mapper.SemesterToSemesterModel(x));
            return semesterModels;
        }
    }
}
