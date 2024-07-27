using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentoroom.DOMAIN.Entities.Shared
{
    public class AssignmentAttachment
    {
        [Key]
        public Guid Id { get; set; }

        public string? Uri { get; set; }
        public string? Name { get; set; }
        public string? ContentType { get; set; }

        [ForeignKey(nameof(Assignment))]
        public Guid AssignmentId { get; set; }
        public CourseAssignment Assignment { get; set; }
    }
}
