using Mentoroom.DOMAIN.Entities.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentoroom.DOMAIN.Entities.StudentModels
{
    public class StudentAssignmentFile
    {
        [Key]
        public Guid Id { get; set; }

        public bool IsSended { get; set; }
        public string? Uri { get; set; }

        [ForeignKey(nameof(StudentAssignment))]
        public Guid StudentAssignmentId { get; set; }
        public StudentAssignment StudentAssignnment { get; set; }

        [ForeignKey(nameof(AssignmentFile))]
        public Guid AssignmentFileId { get; set; }
        public AssignmentFile AssignmentFile { get; set; }

    }
}
