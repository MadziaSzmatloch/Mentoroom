using Mentoroom.APPLICATION.Models;

namespace Mentoroom.APPLICATION.Managements.Course
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool IsActive { get; set; }

        public string Department { get; set; } = string.Empty;
        public string ShortDepartment { get; set; } = string.Empty;
        public string Major { get; set; } = string.Empty;

        public string Degree { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;

        public UserModel Author { get; set; } = default!;
        public List<UserModel> CoAuthors { get; set; } = default!;
    }
}
