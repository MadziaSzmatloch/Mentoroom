using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;

namespace Mentoroom.APPLICATION.Managements.Assignment.Queries.GetAssignmentByStudentId
{
    public class GetAssignmentByStudentIdHandler(IStudentAssignmentRepository studentAssignmentRepository, IAssignmentRepository assignmentRepository) : IRequestHandler<GetAssignmentByStudentId, List<AssignmentDto>>
    {
        public async Task<List<AssignmentDto>> Handle(GetAssignmentByStudentId request, CancellationToken cancellationToken)
        {
            var studentAssignments = (await studentAssignmentRepository.GetAllStudentAssignments()).Where(sa => sa.StudentCourse.StudentId == request.StudentId && sa.IsCompleted == false);

            var courseAssignments = new List<CourseAssignment>();
            foreach (var studentAssignment in studentAssignments)
            {
                courseAssignments.Add(await assignmentRepository.GetAssignmentById(studentAssignment.CourseAssignmentId));
            }

            var mapper = new AssignmentMapper();
            var assignmentDtos = new List<AssignmentDto>();
            foreach (var courseAssignment in courseAssignments)
            {
                assignmentDtos.Add(mapper.AssignmentToAssignmentDto(courseAssignment));
            }
            return assignmentDtos;
        }
    }
}
