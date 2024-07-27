using Mentoroom.APPLICATION.Services.AuthServices.JwtToken;

namespace Mentoroom.APPLICATION.Services.AuthServices
{
    public class AuthResult
    {
        public bool Succeeded { get; set; }
        public JwtTokenDto Token { get; set; }
        public string? ErrorMessage { get; set; } = default!;
    }
}
