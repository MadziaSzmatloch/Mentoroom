namespace Mentoroom.APPLICATION.Models
{
    public class StudentModel
    {
        public string Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? Role { get; set; } = default!;
        public string IndexNumber { get; set; } = default!;
    }
}
