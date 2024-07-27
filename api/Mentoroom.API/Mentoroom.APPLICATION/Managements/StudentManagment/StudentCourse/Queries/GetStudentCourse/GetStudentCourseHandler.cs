using MediatR;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;
using Microsoft.AspNetCore.Identity;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Queries.GetStudentCourse
{
    public class GetStudentCourseHandler(
        UserManager<AppUser> userManager,
        IAssignmentRepository assignmentRepository,
        IStudentAssignmentRepository studentAssignmentRepository,
        IStudentCourseRepository studentCourseRepository,
        ICourseRepository courseRepository,
        IStudentAssignmentFileRepository studentAssignmentFileRepository
        ) : IRequestHandler<GetStudentCourse, List<StudentAssignmentModel>>
    {
        private readonly UserManager<AppUser> userManager = userManager;
        private readonly IAssignmentRepository assignmentRepository = assignmentRepository;
        private readonly IStudentAssignmentRepository studentAssignmentRepository = studentAssignmentRepository;
        private readonly IStudentCourseRepository studentCourseRepository = studentCourseRepository;
        private readonly ICourseRepository courseRepository = courseRepository;
        private readonly IStudentAssignmentFileRepository studentAssignmentFileRepository = studentAssignmentFileRepository;
        public async Task<List<StudentAssignmentModel>> Handle(GetStudentCourse request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.StudentId);
            var course = await courseRepository.GetCourseById(request.CourseId);
            if (course == null)
            {
                throw new Exception("This course does not exist");
            }
            else if (user == null)
            {
                throw new Exception("This user does not exist");
            }
            var studentCourse = (await studentCourseRepository.GetStudentCoursesByCourseId(course.Id)).FirstOrDefault(sc => sc.StudentId == request.StudentId);
            if (studentCourse == null)
            {
                throw new Exception("This student does not belong to this course");
            }

            var studentAssignments = (await studentAssignmentRepository.GetAllStudentAssignments()).Where(st => st.StudentCourseId == studentCourse.Id);

            var studentAssignmentModels = new List<StudentAssignmentModel>();
            foreach (var studentAssignment in studentAssignments)
            {
                var assignment = studentAssignment.CourseAssignment;
                var studentFiles = await studentAssignmentFileRepository.GetAllStudentAssignments();
                var studentAssignmentModel = new StudentAssignmentModel()
                {
                    Id = assignment.Id,
                    Name = assignment.Name,
                    Description = assignment.Description,
                    CreatedDate = assignment.CreatedDate,
                    DeadlineDate = assignment.DeadlineDate,
                    IsActive = assignment.IsActive,
                    CourseId = assignment.CourseId,
                    CourseName = assignment.Course.Name,
                    IsCompleted = studentAssignment.IsCompleted,
                    AssignmentFiles = assignment.AssigmnentAttachments?.Select(a => new AttachmentDto()
                    {
                        Id = a.Id,
                        Name = a.Name,
                    }).ToList(),
                    RequiredFiles = assignment.RequiredFiles?.Select(file => new StudentFileDto()
                    {
                        Id = file.Id,
                        Extension = file.Extension,
                        MaxSizeInKb = file.MaxSizeInKb,
                        FileNameSuffix = file.FileNameSuffix,
                        IsSended = studentFiles.FirstOrDefault(f => f.StudentAssignmentId == studentAssignment.Id && f.AssignmentFileId == file.Id).IsSended,
                    }).ToList(),
                };

                studentAssignmentModels.Add(studentAssignmentModel);
            }
            return studentAssignmentModels;
        }
    }
}
