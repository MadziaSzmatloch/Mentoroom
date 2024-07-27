using MediatR;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;

namespace Mentoroom.APPLICATION.Managements.Assignment.RequiredFiles.DeleteRequiredFile
{
    public class DeleteRequiredFilesHandler(IAssignmentFileRepository assignmentFileRepository, IStudentAssignmentFileRepository studentAssignmentFileRepository, IFileRepository fileRepository) : IRequestHandler<DeleteRequiredFile>
    {
        public async Task Handle(DeleteRequiredFile request, CancellationToken cancellationToken)
        {
            var file = (await assignmentFileRepository.GetAllAssignmentFiles()).FirstOrDefault(f => f.Id == request.RequiredFileId);

            if (file == null)
            {
                throw new Exception("This file does not exist");
            }

            var allStudentFiles = await studentAssignmentFileRepository.GetAllStudentAssignments();
            var studentFiles = allStudentFiles.Where(sf => sf.AssignmentFileId == request.RequiredFileId).ToList();
            foreach (var studentFile in studentFiles)
            {
                if (studentFile.Uri != null)
                {
                    await fileRepository.DeleteByUri(studentFile.Uri);
                }
                await studentAssignmentFileRepository.DeleteFile(studentFile.Id);
            }
            await assignmentFileRepository.DeleteFile(file);

        }
    }
}
