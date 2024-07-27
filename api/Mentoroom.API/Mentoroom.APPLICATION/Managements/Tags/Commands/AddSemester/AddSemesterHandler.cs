using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Interfaces.Tags;

namespace Mentoroom.APPLICATION.Managements.Tags.Commands.AddSemester
{
    public class AddSemesterHandler(ISemesterRepository semesterRepository) : IRequestHandler<AddSemester, SemesterModel>
    {
        public async Task<SemesterModel> Handle(AddSemester request, CancellationToken cancellationToken)
        {
            var semester = await semesterRepository.AddSemester(new DOMAIN.Entities.Tags.Semester()
            {
                Name = request.Name,
                YearId = request.YearId,
            });
            var mapper = new TagsMapper();
            return mapper.SemesterToSemesterModel(semester);
        }
    }
}
