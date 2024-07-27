using MediatR;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Models;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentFiles.Queries.DownloadStudentFilesByAssignment
{
    public class DownloadStudentFilesByAssignmentHandler(IFileRepository fileRepository, IAssignmentRepository assignmentRepository) : IRequestHandler<DownloadStudentFilesByAssignment, Blob>
    {
        private readonly IFileRepository fileRepository = fileRepository;
        private readonly IAssignmentRepository assignmentRepository = assignmentRepository;

        public async Task<Blob> Handle(DownloadStudentFilesByAssignment request, CancellationToken cancellationToken)
        {
            var assignemnt = await assignmentRepository.GetAssignmentById(request.AssignmentId);
            if (assignemnt == null)
            {
                throw new Exception("There is no assignment with this id");
            }
            var list = await fileRepository.BlobList();
            if (list.Any(b => b.Name.Contains($"studentfiles/{assignemnt.Course.Name}/{assignemnt.Name}")) == false)
            {
                throw new Exception("This assignment does not have any files");
            }
            var result = await fileRepository.DownloadFolder($"studentfiles/{assignemnt.Course.Name}/{assignemnt.Name}");
            if (result == null)
            {
                throw new Exception("Something went wrong");
            }
            return result;
        }
    }
}
