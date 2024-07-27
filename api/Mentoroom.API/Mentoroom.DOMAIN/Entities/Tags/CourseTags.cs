using System.ComponentModel.DataAnnotations;

namespace Mentoroom.DOMAIN.Entities.Tags
{
    public class CourseTags
    {
        [Key]
        public Guid Id { get; set; }

        public string Department { get; set; } = string.Empty;
        public string ShortDepartment { get; set; } = string.Empty;
        public string Major { get; set; } = string.Empty;

        public string Degree { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
    }
}
