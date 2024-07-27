using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;

namespace Mentoroom.APPLICATION.Managements.Assignment.RequiredFiles.AddRequiredFile
{
    public class AddRequiredFileHandler(IAssignmentFileRepository assignmentFileRepository,
        IAssignmentRepository assignmentRepository,
        IStudentAssignmentRepository studentAssignmentRepository,
        IStudentAssignmentFileRepository studentAssignmentFileRepository) : IRequestHandler<AddRequiredFile, AssignmentDto>
    {
        private readonly IAssignmentFileRepository assignmentFileRepository = assignmentFileRepository;
        private readonly IAssignmentRepository assignmentRepository = assignmentRepository;
        private readonly IStudentAssignmentRepository studentAssignmentRepository = studentAssignmentRepository;
        private readonly IStudentAssignmentFileRepository studentAssignmentFileRepository = studentAssignmentFileRepository;

        public async Task<AssignmentDto> Handle(AddRequiredFile request, CancellationToken cancellationToken)
        {
            var assignment = await assignmentRepository.GetAssignmentById(request.AssignmentId);
            if (assignment == null)
            {
                throw new Exception("This assignment does not exist");
            }

            var file = await assignmentFileRepository.AddFile(new DOMAIN.Entities.Shared.AssignmentFile()
            {
                Extension = request.Extension,
                MaxSizeInKb = request.MaxSizeInKb,
                FileNameSuffix = request.FileNameSuffix,
                AssignmentId = request.AssignmentId,
            });


            var studentAssignments = (await studentAssignmentRepository.GetAllStudentAssignments()).Where(a => a.CourseAssignmentId == assignment.Id);
            foreach (var studentAssignment in studentAssignments)
            {
                await studentAssignmentFileRepository.AddStudentFile(new DOMAIN.Entities.StudentModels.StudentAssignmentFile()
                {
                    IsSended = false,
                    StudentAssignmentId = studentAssignment.Id,
                    AssignmentFileId = file.Id,
                    Uri = null,
                });

            }

            assignment = await assignmentRepository.GetAssignmentById(request.AssignmentId);
            var assignmentMapper = new AssignmentMapper();
            var assignmentDto = assignmentMapper.AssignmentToAssignmentDto(assignment);
            return assignmentDto;
        }
    }
}
