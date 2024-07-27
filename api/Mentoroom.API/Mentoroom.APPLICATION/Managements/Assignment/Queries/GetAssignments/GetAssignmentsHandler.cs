using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.DOMAIN.Interfaces.Shared;

namespace Mentoroom.APPLICATION.Managements.Assignment.Queries.GetAssignments
{
    public class GetAssignmentsHandler(IAssignmentRepository assignmentRepository, IFileRepository fileRepository) : IRequestHandler<GetAssignments, IEnumerable<AssignmentDto>>
    {
        private readonly IAssignmentRepository assignmentRepository = assignmentRepository;
        private readonly IFileRepository fileRepository = fileRepository;

        public async Task<IEnumerable<AssignmentDto>> Handle(GetAssignments request, CancellationToken cancellationToken)
        {
            var assignments = await assignmentRepository.GetAllAssignments();

            if (assignments.Count() == 0)
            {
                throw new Exception("There are no assignments in the database");
            }

            var assignmentMapper = new AssignmentMapper();
            var assignmentDtos = assignments.Select(assignment => assignmentMapper.AssignmentToAssignmentDto(assignment)).ToList();

            //foreach (var assignmentDto in assignmentDtos)
            //{
            //    var files = (await fileRepository.BlobList()).Select(x => x.Name).Where(x => x.StartsWith($"assignmentfiles/{assignmentDto.CourseName}/{assignmentDto.Name}/"));
            //    assignmentDto.AssignmentFiles = files.ToList();
            //}

            return assignmentDtos;
        }
    }
}
