using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Tags.Commands.AddDepartment
{
    public class AddDepartment : IRequest<DepartmentModel>
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
