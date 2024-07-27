using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;

namespace Mentoroom.APPLICATION.Managements.Assignment.Commands.AddAssignment
{
    public class AddAssignmentHandler(IAssignmentRepository assignmentRepository,
        ICourseRepository courseRepository,
        IAssignmentFileRepository assignmentFileRepository,
        IStudentAssignmentRepository studentAssignmentRepository,
        IStudentCourseRepository studentCourseRepository,
        IStudentAssignmentFileRepository studentAssignmentFileRepository
        ) : IRequestHandler<AddAssignment, AssignmentDto>
    {
        private readonly IAssignmentRepository assignmentRepository = assignmentRepository;
        private readonly ICourseRepository courseRepository = courseRepository;
        private readonly IAssignmentFileRepository assignmentFileRepository = assignmentFileRepository;
        private readonly IStudentAssignmentRepository studentAssignmentRepository = studentAssignmentRepository;
        private readonly IStudentCourseRepository studentCourseRepository = studentCourseRepository;
        private readonly IStudentAssignmentFileRepository studentAssignmentFileRepository = studentAssignmentFileRepository;

        public async Task<AssignmentDto> Handle(AddAssignment request, CancellationToken cancellationToken)
        {
            var courseAssignments = await assignmentRepository.GetAssignmentsByCourseId(request.CourseId);
            var sameNameAssignment = courseAssignments.FirstOrDefault(a => a.Name == request.Name);
            if (sameNameAssignment != null)
            {
                throw new Exception("There is assignment with this name in this course, choose a different name");
            }

            var assignment = await assignmentRepository.AddAssignment(new DOMAIN.Entities.Shared.CourseAssignment
            {
                Name = request.Name,
                Description = request.Description,
                CreatedDate = DateTime.Now,
                DeadlineDate = request.DeadlineDate,
                IsActive = request.isActive,
                CourseId = request.CourseId,
                Course = await courseRepository.GetCourseById(request.CourseId),
            });

            foreach (var file in request.Files)
            {
                await assignmentFileRepository.AddFile(new DOMAIN.Entities.Shared.AssignmentFile()
                {
                    Extension = file.Extension,
                    MaxSizeInKb = file.MaxSizeInKb,
                    FileNameSuffix = file.FileNameSuffix,
                    AssignmentId = assignment.Id,
                });
            }

            var files = (await assignmentFileRepository.GetAllAssignmentFiles()).Where(f => f.AssignmentId == assignment.Id);
            foreach (var student in assignment.Course.Students)
            {
                var studentAssignment = await studentAssignmentRepository.AddStudentAssignmnent(new DOMAIN.Entities.StudentModels.StudentAssignment()
                {
                    IsCompleted = false,
                    StudentCourseId = (await studentCourseRepository.GetAllStudentCourses())
                                      .FirstOrDefault(sc => sc.CourseId == request.CourseId && sc.StudentId.ToString() == student.StudentId.ToString()).Id,
                    CourseAssignmentId = assignment.Id,
                });

                foreach (var file in files)
                {
                    await studentAssignmentFileRepository.AddStudentFile(new DOMAIN.Entities.StudentModels.StudentAssignmentFile()
                    {
                        IsSended = false,
                        StudentAssignmentId = studentAssignment.Id,
                        AssignmentFileId = file.Id
                    });
                }
            }

            var assignmentMapper = new AssignmentMapper();
            var assignmentDto = assignmentMapper.AssignmentToAssignmentDto(assignment);
            return assignmentDto;
        }
    }
}
