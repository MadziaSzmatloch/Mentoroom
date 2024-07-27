namespace Mentoroom.APPLICATION.Managements.Course
{
    public class CoursePost
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Guid DegreeId { get; set; }
        public Guid YearId { get; set; }
        public Guid SemesterId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid MajorId { get; set; }

        public string AuthorId { get; set; }
        public List<string> CoAuthorsId { get; set; }
    }
}
