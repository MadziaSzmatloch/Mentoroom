using MediatR;
using Mentoroom.APPLICATION.Services.AuthServices.JwtToken;

namespace Mentoroom.APPLICATION.Managements.Auth.Register
{
    public class Register : IRequest<JwtTokenDto>
    {
        public string FistName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string PasswordConfirm { get; set; } = default!;
    }
}
