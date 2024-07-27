using Mentoroom.DOMAIN.Entities.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentoroom.DOMAIN.Entities.StudentModels
{
    public class StudentCourse
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime JoiningDate { get; set; }

        [ForeignKey(nameof(Student))]
        public string StudentId { get; set; }
        public Student Student { get; set; }

        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public bool? IsConfirmed { get; set; } = null;
        public List<StudentAssignment> StudentAssignments { get; set; }
    }
}
