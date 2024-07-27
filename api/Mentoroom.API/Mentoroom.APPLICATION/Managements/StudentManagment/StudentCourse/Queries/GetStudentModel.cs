namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Queries
{
    public class GetStudentModel
    {
        public string Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? Role { get; set; } = default!;
        public bool? IsConfirmed { get; set; }
    }
}
