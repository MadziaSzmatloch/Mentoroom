using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Entities.Shared;
using Riok.Mapperly.Abstractions;

namespace Mentoroom.APPLICATION.Mappings
{
    [Mapper]
    public partial class AssignmentMapper
    {
        public AssignmentDto AssignmentToAssignmentDto(CourseAssignment assignment)
        {
            var assignmnetDto = new AssignmentDto()
            {
                Id = assignment.Id,
                Name = assignment.Name,
                Description = assignment.Description,
                CreatedDate = assignment.CreatedDate,
                DeadlineDate = assignment.DeadlineDate,
                IsActive = assignment.IsActive,
                CourseName = assignment.Course.Name,
                AssignmentFiles = assignment.AssigmnentAttachments?.Select(a => new AttachmentDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                }).ToList(),
                RequiredFiles = assignment.RequiredFiles?.Select(f => new FileModel()
                {
                    Id = f.Id,
                    Extension = f.Extension,
                    MaxSizeInKb = f.MaxSizeInKb,
                    FileNameSuffix = f.FileNameSuffix
                }).ToList(),
            };
            return assignmnetDto;
        }
    }
}
