using Mentoroom.DOMAIN.Entities.StudentModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentoroom.DOMAIN.Entities.Shared
{
    public class CourseAssignment
    {
        public CourseAssignment()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
            DeadlineDate = CreatedDate.AddDays(7);
        }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public bool IsActive { get; set; } = true;

        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public List<AssignmentFile> RequiredFiles { get; set; }
        public List<StudentAssignment> StudentAssignments { get; set; }
        public List<AssignmentAttachment> AssigmnentAttachments { get; set; }
    }
}
