using MediatR;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.DOMAIN.Interfaces.Shared;

namespace Mentoroom.APPLICATION.Managements.Assignment.Files.DeleteAssignmentFile
{
    public class DeleteAssignmentFileHandler(IAssignmentAttachmentRepository assignmentAttachmentRepository, IFileRepository fileRepository) : IRequestHandler<DeleteAssignmentFile>
    {
        private readonly IAssignmentAttachmentRepository assignmentAttachmentRepository = assignmentAttachmentRepository;
        private readonly IFileRepository fileRepository = fileRepository;

        public async Task Handle(DeleteAssignmentFile request, CancellationToken cancellationToken)
        {
            var attachment = (await assignmentAttachmentRepository.GetAllAttachments()).FirstOrDefault(a => a.Id == request.AttachmentId);
            if (attachment == null)
            {
                throw new Exception("There is no attachment with this id");
            }

            try
            {
                await fileRepository.DeleteByUri(attachment.Uri);
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong");
            }

            await assignmentAttachmentRepository.DeleteAttachment(attachment);
        }
    }
}
