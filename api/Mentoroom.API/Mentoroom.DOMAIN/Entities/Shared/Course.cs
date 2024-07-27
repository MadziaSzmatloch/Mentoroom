using Mentoroom.DOMAIN.Entities.LecturerModels;
using Mentoroom.DOMAIN.Entities.StudentModels;
using Mentoroom.DOMAIN.Entities.Tags;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Mentoroom.DOMAIN.Entities.Shared
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; } = true;

        [ForeignKey(nameof(Tags))]
        public Guid TagsId { get; set; }
        public CourseTags Tags { get; set; }

        [ForeignKey(nameof(Author))]
        public string AuthorId { get; set; }
        public AppUser Author { get; set; }


        public List<CourseCoAuthor> CoAuthors { get; set; }
        public List<CourseAssignment> Assignments { get; set; }
        public List<StudentCourse> Students { get; set; }


    }
}
