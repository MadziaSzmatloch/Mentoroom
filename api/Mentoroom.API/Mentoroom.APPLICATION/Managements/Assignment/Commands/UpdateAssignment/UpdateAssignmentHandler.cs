using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces.Shared;

namespace Mentoroom.APPLICATION.Managements.Assignment.Commands.UpdateAssignment
{
    public class UpdateAssignmentHandler(IAssignmentRepository assignmentRepository) : IRequestHandler<UpdateAssignment, AssignmentDto>
    {
        public async Task<AssignmentDto> Handle(UpdateAssignment request, CancellationToken cancellationToken)
        {
            var assignment = await assignmentRepository.GetAssignmentById(request.Id) ?? throw new Exception("This assignment does not exist");
            var updateAssignment = new CourseAssignment()
            {
                Id = request.Id,
                Name = assignment.Name,
                Description = request.Description,
                CreatedDate = assignment.CreatedDate,
                DeadlineDate = request.DeadlineDate,
                IsActive = request.isActive,
                CourseId = assignment.CourseId,
                Course = assignment.Course,
            };

            updateAssignment = await assignmentRepository.UpdateAssignment(updateAssignment);
            updateAssignment = await assignmentRepository.GetAssignmentById(updateAssignment.Id);

            var assignmentMapper = new AssignmentMapper();
            var assignmentDto = assignmentMapper.AssignmentToAssignmentDto(updateAssignment);
            return assignmentDto;

        }
    }
}
