using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Interfaces.Tags;

namespace Mentoroom.APPLICATION.Managements.Tags.Queries.Semester.GetSemesterByYear
{
    internal class GetSemesterByYearHandler(ISemesterRepository semesterRepository, IYearRepository yearRepository) : IRequestHandler<GetSemesterByYear, IEnumerable<SemesterModel>>
    {
        public async Task<IEnumerable<SemesterModel>> Handle(GetSemesterByYear request, CancellationToken cancellationToken)
        {
            var year = await yearRepository.GetYearById(request.YearId);
            if (year == null)
            {
                throw new Exception("This year does not exist");
            }
            var mapper = new TagsMapper();
            var semesters = await semesterRepository.GetSemestersByYear(request.YearId);
            if (semesters.Any() == false)
            {
                throw new Exception("There are no semesters in the database");
            }
            var semesterModel = semesters.Select(x => mapper.SemesterToSemesterModel(x));
            return semesterModel;
        }
    }
}
