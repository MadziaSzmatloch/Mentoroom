using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.DOMAIN.Interfaces.Shared;

namespace Mentoroom.APPLICATION.Managements.Assignment.Queries.GetAssignmentById
{
    public class GetAssignmentByCourseIdHandler(IAssignmentRepository assignmentRepository, ICourseRepository courseRepository, IFileRepository fileRepository) : IRequestHandler<GetAssignmentByCourseId, IEnumerable<AssignmentDto>>
    {
        private readonly IAssignmentRepository assignmentRepository = assignmentRepository;
        private readonly ICourseRepository courseRepository = courseRepository;
        private readonly IFileRepository fileRepository = fileRepository;

        public async Task<IEnumerable<AssignmentDto>> Handle(GetAssignmentByCourseId request, CancellationToken cancellationToken)
        {
            var course = await courseRepository.GetCourseById(request.CourseId) ?? throw new Exception("This course does not exist");
            var assignments = await assignmentRepository.GetAssignmentsByCourseId(request.CourseId);
            if (!assignments.Any())
            {
                throw new Exception("This course does not have any assignments");
            }
            var assignmentMapper = new AssignmentMapper();
            var assignmentDtos = assignments.Select(assignment => assignmentMapper.AssignmentToAssignmentDto(assignment)).ToList();

            return assignmentDtos;
        }
    }
}
