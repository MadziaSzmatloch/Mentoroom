using MediatR;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;
using Microsoft.AspNetCore.Identity;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentFiles.Commands.Upload
{
    public class UploadStudentFileHandler(IFileRepository fileRepository,
        IAssignmentFileRepository assignmentFileRepository,
        IAssignmentRepository assignmentRepository,
        ICourseRepository courseRepository,
        IStudentAssignmentFileRepository studentAssignmentFileRepository,
        IStudentAssignmentRepository studentAssignmentRepository,
        UserManager<AppUser> userManager) : IRequestHandler<UploadStudentFile, StudentAssignmentModel>
    {
        private readonly IFileRepository fileRepository = fileRepository;
        private readonly IAssignmentFileRepository assignmentFileRepository = assignmentFileRepository;
        private readonly IAssignmentRepository assignmentRepository = assignmentRepository;
        private readonly ICourseRepository courseRepository = courseRepository;
        private readonly IStudentAssignmentFileRepository studentAssignmentFileRepository = studentAssignmentFileRepository;
        private readonly IStudentAssignmentRepository studentAssignmentRepository = studentAssignmentRepository;
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task<StudentAssignmentModel> Handle(UploadStudentFile request, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            var assignmentFile = (await assignmentFileRepository.GetAllAssignmentFiles()).FirstOrDefault(af => af.Id == request.AssignmentFileId);
            if (assignmentFile == null)
            {
                throw new Exception("Wrong assignment file id");
            }

            var extension = Path.GetExtension(request.File.FileName).ToLower();
            if (extension.ToUpper() != $".{assignmentFile.Extension.ToUpper()}")
            {
                throw new Exception("Wrong file extension");
            }
            if (request.File.Length > assignmentFile.MaxSizeInKb * 1024)
            {
                throw new Exception("Wrong file size");
            }


            var assignment = (await assignmentRepository.GetAssignmentById(assignmentFile.AssignmentId));
            var course = (await courseRepository.GetCourseById(assignment.CourseId));
            var student = await userManager.FindByIdAsync(request.StudentId);
            if (student == null)
            {
                throw new Exception("This student does not exist");
            }

            var studentAssignments = (await studentAssignmentRepository.GetAllStudentAssignments());

            var studentAssignment = studentAssignments.FirstOrDefault(sa => sa.StudentCourse.StudentId == student.Id && sa.StudentCourse.CourseId == course.Id && sa.CourseAssignmentId == assignment.Id);

            if (studentAssignment == null)
            {
                throw new Exception("This student does not belong to this assignment");
            }
            var blob = await fileRepository.UploadFile(request.File, $"studentfiles/{course.Name}/{assignment.Name}/{student.Email}/{assignmentFile.FileNameSuffix}");
            try
            {
                await studentAssignmentFileRepository.SendFile(studentAssignment.Id, request.AssignmentFileId, blob.Uri);
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong");
            }


            var notSendedFiles = studentAssignment.StudentFiles.Where(f => f.IsSended == false);
            if (notSendedFiles.Any() == false)
            {
                await studentAssignmentRepository.ChangeCompletedToTrue(studentAssignment.Id);
            }

            var studentFiles = await studentAssignmentFileRepository.GetAllStudentAssignments();
            var studentAssignmentModel = new StudentAssignmentModel()
            {
                Id = assignment.Id,
                Name = assignment.Name,
                Description = assignment.Description,
                CreatedDate = assignment.CreatedDate,
                DeadlineDate = assignment.DeadlineDate,
                IsActive = assignment.IsActive,
                CourseId = course.Id,
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

            return studentAssignmentModel;
        }
    }
}
