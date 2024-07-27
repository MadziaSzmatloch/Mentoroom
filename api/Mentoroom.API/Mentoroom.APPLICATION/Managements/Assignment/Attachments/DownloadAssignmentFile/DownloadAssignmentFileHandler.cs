using MediatR;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Models;

namespace Mentoroom.APPLICATION.Managements.Assignment.Files.DownloadAssignmentFile
{
    public class DownloadAssignmentFileHandler(IAssignmentAttachmentRepository assignmentAttachmentRepository, IFileRepository fileRepository) : IRequestHandler<DownloadAssignmentFile, Blob>
    {
        private readonly IAssignmentAttachmentRepository assignmentAttachmentRepository = assignmentAttachmentRepository;
        private readonly IFileRepository fileRepository = fileRepository;

        public async Task<Blob> Handle(DownloadAssignmentFile request, CancellationToken cancellationToken)
        {
            var attachment = (await assignmentAttachmentRepository.GetAllAttachments()).FirstOrDefault(a => a.Id == request.AttachmentId);
            if (attachment == null)
            {
                throw new Exception("There is no attachment with this id");
            }
            var blob = await fileRepository.Download(attachment.Uri);
            if (blob == null)
            {
                throw new Exception("Something went wrong");
            }
            return blob;
        }
    }
}
