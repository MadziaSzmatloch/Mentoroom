using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Assignment.RequiredFiles.UpdateRequiredFile
{
    public class UpdateRequiredFile : IRequest<AssignmentDto>
    {
        public FileModel FileModel { get; set; }
    }
}
