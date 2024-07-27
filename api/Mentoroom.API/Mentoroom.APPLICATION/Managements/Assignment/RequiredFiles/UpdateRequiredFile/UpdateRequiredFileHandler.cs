using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Interfaces.Shared;

namespace Mentoroom.APPLICATION.Managements.Assignment.RequiredFiles.UpdateRequiredFile
{
    public class UpdateRequiredFileHandler(IAssignmentFileRepository assignmentFileRepository, IAssignmentRepository assignmentRepository) : IRequestHandler<UpdateRequiredFile, AssignmentDto>
    {
        private readonly IAssignmentFileRepository assignmentFileRepository = assignmentFileRepository;
        private readonly IAssignmentRepository assignmentRepository = assignmentRepository;

        public async Task<AssignmentDto> Handle(UpdateRequiredFile request, CancellationToken cancellationToken)
        {
            var file = (await assignmentFileRepository.GetAllAssignmentFiles()).FirstOrDefault(f => f.Id == request.FileModel.Id);
            if (file == null)
            {
                throw new Exception("This file does not exist");
            }

            var updatedFile = await assignmentFileRepository.UpdateFile(new DOMAIN.Entities.Shared.AssignmentFile()
            {
                Id = request.FileModel.Id,
                Extension = request.FileModel.Extension,
                MaxSizeInKb = request.FileModel.MaxSizeInKb,
                FileNameSuffix = request.FileModel.FileNameSuffix,
                AssignmentId = file.AssignmentId,
            });

            var assignment = await assignmentRepository.GetAssignmentById(file.AssignmentId);
            var assignmentMapper = new AssignmentMapper();
            var assignmentDto = assignmentMapper.AssignmentToAssignmentDto(assignment);
            return assignmentDto;
        }
    }
}
