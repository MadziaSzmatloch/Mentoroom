using MediatR;
using Mentoroom.APPLICATION.Services.AuthServices;
using Mentoroom.APPLICATION.Services.AuthServices.JwtToken;

namespace Mentoroom.APPLICATION.Managements.Auth.Login
{
    public class LoginHandler(IAuthService authService) : IRequestHandler<Login, JwtTokenDto>
    {
        private readonly IAuthService authService = authService;

        public async Task<JwtTokenDto> Handle(Login request, CancellationToken cancellationToken)
        {
            var result = await authService.SignIn(request);
            return result;
        }
    }
}
