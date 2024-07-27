using Mentoroom.DOMAIN.Entities.StudentModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentoroom.DOMAIN.Entities.Shared
{
    public class AssignmentFile
    {
        [Key]
        public Guid Id { get; set; }
        public string Extension { get; set; }
        public int MaxSizeInKb { get; set; }
        public string FileNameSuffix { get; set; }

        [ForeignKey(nameof(Assignment))]
        public Guid AssignmentId { get; set; }
        public CourseAssignment Assignment { get; set; }
        public List<StudentAssignmentFile> StudentsFiles { get; set; }

    }
}
