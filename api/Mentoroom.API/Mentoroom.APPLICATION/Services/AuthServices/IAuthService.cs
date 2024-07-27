using Mentoroom.APPLICATION.Managements.Auth.Login;
using Mentoroom.APPLICATION.Managements.Auth.Logout;
using Mentoroom.APPLICATION.Managements.Auth.Register;
using Mentoroom.APPLICATION.Services.AuthServices.JwtToken;

namespace Mentoroom.APPLICATION.Services.AuthServices
{
    public interface IAuthService
    {
        Task<JwtTokenDto> RefreshToken(JwtTokenDto request);
        Task<JwtTokenDto> SignIn(Login request);
        Task<JwtTokenDto> SignUp(Register request);
        Task<bool> Logout(Logout request);
        Task<bool> PromoteTo(string role, string userId);
    }
}