using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Tags.Commands.AddSemester
{
    public class AddSemester : IRequest<SemesterModel>
    {
        public string Name { get; set; }
        public Guid YearId { get; set; }
    }
}
