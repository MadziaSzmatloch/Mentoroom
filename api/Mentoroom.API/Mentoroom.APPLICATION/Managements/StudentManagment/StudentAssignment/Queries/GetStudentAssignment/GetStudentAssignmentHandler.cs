using MediatR;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;
using Microsoft.AspNetCore.Identity;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentAssignment.Queries.GetStudentAssignment
{
    public class GetStudentAssignmentHandler(
        UserManager<AppUser> userManager,
        IAssignmentRepository assignmentRepository,
        IStudentAssignmentRepository studentAssignmentRepository,
        IStudentCourseRepository studentCourseRepository,
        ICourseRepository courseRepository,
        IStudentAssignmentFileRepository studentAssignmentFileRepository
        ) : IRequestHandler<GetStudentAssignment, StudentAssignmentModel>
    {
        private readonly UserManager<AppUser> userManager = userManager;
        private readonly IAssignmentRepository assignmentRepository = assignmentRepository;
        private readonly IStudentAssignmentRepository studentAssignmentRepository = studentAssignmentRepository;
        private readonly IStudentCourseRepository studentCourseRepository = studentCourseRepository;
        private readonly ICourseRepository courseRepository = courseRepository;
        private readonly IStudentAssignmentFileRepository studentAssignmentFileRepository = studentAssignmentFileRepository;

        public async Task<StudentAssignmentModel> Handle(GetStudentAssignment request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.StudentId);
            var assignment = await assignmentRepository.GetAssignmentById(request.AssignmnetId);
            if (assignment == null)
            {
                throw new Exception("This assignment does not exist");
            }
            else if (user == null)
            {
                throw new Exception("This user does not exist");
            }
            var studentCourse = (await studentCourseRepository.GetStudentCoursesByCourseId(assignment.CourseId)).FirstOrDefault(sc => sc.StudentId == request.StudentId);
            if (studentCourse == null)
            {
                throw new Exception("This student does not belong to this course");
            }
            var studentAssignment = (await studentAssignmentRepository.GetAllStudentAssignments()).FirstOrDefault(sa => sa.CourseAssignmentId == request.AssignmnetId && sa.StudentCourseId == studentCourse.Id);
            if (studentAssignment == null)
            {
                throw new Exception("This student does not belong to this assignment");
            }

            var studentFiles = await studentAssignmentFileRepository.GetAllStudentAssignments();
            var studentAssignmentModel = new StudentAssignmentModel()
            {
                Name = assignment.Name,
                Description = assignment.Description,
                CreatedDate = assignment.CreatedDate,
                DeadlineDate = assignment.DeadlineDate,
                IsActive = assignment.IsActive,
                CourseName = assignment.Course.Name,
                IsCompleted = studentAssignment.IsCompleted,
                AssignmentFiles = assignment.AssigmnentAttachments?.Select(a => new AttachmentDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                }).ToList(),
                RequiredFiles = assignment.RequiredFiles?.Select(file => new StudentFileDto()
                {
                    Id = studentFiles.FirstOrDefault(f => f.StudentAssignmentId == studentAssignment.Id && f.AssignmentFileId == file.Id).Id,
                    Extension = file.Extension,
                    MaxSizeInKb = file.MaxSizeInKb,
                    FileNameSuffix = file.FileNameSuffix,
                    IsSended = studentFiles.FirstOrDefault(f => f.StudentAssignmentId == studentAssignment.Id && f.AssignmentFileId == file.Id).IsSended,
                }).ToList(),
            };
            //var mapper = new StudentAssignmentMapper();
            //var studentAssignmentModel = mapper.StudentAssignmentToStudentAssignmentModel(studentAssignment);

            return studentAssignmentModel;
        }
    }
}
