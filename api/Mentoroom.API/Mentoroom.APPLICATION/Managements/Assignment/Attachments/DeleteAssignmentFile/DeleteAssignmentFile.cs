using MediatR;

namespace Mentoroom.APPLICATION.Managements.Assignment.Files.DeleteAssignmentFile
{
    public class DeleteAssignmentFile : IRequest
    {
        public Guid AttachmentId { get; set; }
    }
}
