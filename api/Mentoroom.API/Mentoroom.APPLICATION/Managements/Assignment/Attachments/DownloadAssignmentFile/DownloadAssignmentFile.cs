using MediatR;
using Mentoroom.DOMAIN.Models;

namespace Mentoroom.APPLICATION.Managements.Assignment.Files.DownloadAssignmentFile
{
    public class DownloadAssignmentFile : IRequest<Blob>
    {
        public Guid AttachmentId { get; set; }
    }
}
