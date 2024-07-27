using MediatR;
using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Assignment.RequiredFiles.AddRequiredFile
{
    public class AddRequiredFile : IRequest<AssignmentDto>
    {
        public Guid AssignmentId { get; set; }
        public string Extension { get; set; }
        public int MaxSizeInKb { get; set; }
        public string FileNameSuffix { get; set; }
    }
}
