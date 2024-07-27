namespace Mentoroom.APPLICATION.Models
{
    public class UserModel
    {
        public string Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? Role { get; set; } = default!;
    }
}
