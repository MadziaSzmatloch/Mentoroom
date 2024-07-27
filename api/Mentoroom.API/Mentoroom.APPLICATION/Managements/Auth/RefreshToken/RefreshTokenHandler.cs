using MediatR;
using Mentoroom.APPLICATION.Services.AuthServices;
using Mentoroom.APPLICATION.Services.AuthServices.JwtToken;

namespace Mentoroom.APPLICATION.Managements.Auth.RefreshToken
{
    public class RefreshTokenHandler(IAuthService authService) : IRequestHandler<RefreshToken, JwtTokenDto>
    {
        private readonly IAuthService authService = authService;
        public async Task<JwtTokenDto> Handle(RefreshToken request, CancellationToken cancellationToken)
        {
            var result = await authService.RefreshToken(request);
            return result;
        }
    }
}
