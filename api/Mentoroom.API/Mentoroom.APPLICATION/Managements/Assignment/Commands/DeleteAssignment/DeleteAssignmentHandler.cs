using MediatR;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;

namespace Mentoroom.APPLICATION.Managements.Assignment.Commands.DeleteAssignment
{
    public class DeleteAssignmentHandler(IAssignmentRepository assignmentRepository,
        IAssignmentAttachmentRepository assignmentAttachmentRepository,
        IFileRepository fileRepository,
        IStudentAssignmentFileRepository studentAssignmentFileRepository) : IRequestHandler<DeleteAssignment>
    {
        public async Task Handle(DeleteAssignment request, CancellationToken cancellationToken)
        {
            var assignment = await assignmentRepository.GetAssignmentById(request.AssignmentId) ?? throw new Exception("This assignment does not exist");

            var allAtachments = await assignmentAttachmentRepository.GetAllAttachments();
            var attachments = allAtachments.Where(a => a.AssignmentId == assignment.Id).ToList();
            foreach (var attachment in attachments)
            {

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

            var allStudentFiles = await studentAssignmentFileRepository.GetAllStudentAssignments();
            var studentFiles = allStudentFiles.Where(f => f.StudentAssignnment.CourseAssignmentId == assignment.Id).ToList();
            studentFiles = studentFiles.Where(f => f.IsSended == true).ToList();
            foreach (var studentFile in studentFiles)
            {
                try
                {
                    await fileRepository.DeleteByUri(studentFile.Uri);
                }
                catch (Exception)
                {
                    throw new Exception("Something went wrong");
                }
            }
            await assignmentRepository.DeleteAssignmentById(assignment);
        }
    }
}
