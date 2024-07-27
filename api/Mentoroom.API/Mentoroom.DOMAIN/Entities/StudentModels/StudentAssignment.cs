using Mentoroom.DOMAIN.Entities.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentoroom.DOMAIN.Entities.StudentModels
{
    public class StudentAssignment
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsCompleted { get; set; } = false;

        [ForeignKey(nameof(Student))]
        public Guid StudentCourseId { get; set; }
        public StudentCourse StudentCourse { get; set; }

        [ForeignKey(nameof(Course))]
        public Guid CourseAssignmentId { get; set; }
        public CourseAssignment CourseAssignment { get; set; }
        public List<StudentAssignmentFile> StudentFiles { get; set; }

    }
}
