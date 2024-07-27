using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Entities.StudentModels;
using Riok.Mapperly.Abstractions;

namespace Mentoroom.APPLICATION.Mappings
{
    [Mapper]
    public partial class StudentAssignmentMapper
    {
        //[MapProperty(nameof(StudentCourse.Course), nameof(StudentCourseDto.CourseDto))]
        public StudentAssignmentModel StudentAssignmentToStudentAssignmentModel(StudentAssignment studentAssignment)
        {
            var studentAssignmentModel = new StudentAssignmentModel()
            {

                Name = studentAssignment.CourseAssignment.Name,
                Description = studentAssignment.CourseAssignment.Description,
                CreatedDate = studentAssignment.CourseAssignment.CreatedDate,
                DeadlineDate = studentAssignment.CourseAssignment.DeadlineDate,
                IsActive = studentAssignment.CourseAssignment.IsActive,
                CourseName = studentAssignment.CourseAssignment.Course.Name,
                IsCompleted = studentAssignment.IsCompleted,
                AssignmentFiles = studentAssignment.CourseAssignment.AssigmnentAttachments?.Select(a => new AttachmentDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                }).ToList(),
                RequiredFiles = studentAssignment.CourseAssignment.RequiredFiles?.Select(file => new StudentFileDto()
                {
                    Id = studentAssignment.StudentFiles.FirstOrDefault(f => f.StudentAssignmentId == studentAssignment.Id && f.AssignmentFileId == file.Id).Id,
                    Extension = file.Extension,
                    MaxSizeInKb = file.MaxSizeInKb,
                    FileNameSuffix = file.FileNameSuffix,
                    IsSended = studentAssignment.StudentFiles.FirstOrDefault(f => f.StudentAssignmentId == studentAssignment.Id && f.AssignmentFileId == file.Id).IsSended,
                }).ToList(),
            };

            return studentAssignmentModel;
        }
    }
}
