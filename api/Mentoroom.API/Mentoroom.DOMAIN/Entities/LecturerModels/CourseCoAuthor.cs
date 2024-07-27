using Mentoroom.DOMAIN.Entities.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentoroom.DOMAIN.Entities.LecturerModels
{
    public class CourseCoAuthor
    {
        [ForeignKey(nameof(AppUser))]
        public string CoAuthorId { get; set; }
        public AppUser CoAuthor { get; set; }

        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}
