using MediatR;
using Mentoroom.APPLICATION.Services.AuthServices.JwtToken;

namespace Mentoroom.APPLICATION.Managements.Auth.Login
{
    public class Login : IRequest<JwtTokenDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
