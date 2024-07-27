using Mentoroom.DOMAIN.Entities.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentoroom.DOMAIN.Entities.StudentModels
{
    public class Student
    {
        public string IndexNumber { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public List<StudentCourse> Courses { get; set; }
    }
}
