using MediatR;
using Mentoroom.APPLICATION.Services.AuthServices;
using Mentoroom.APPLICATION.Services.AuthServices.JwtToken;

namespace Mentoroom.APPLICATION.Managements.Auth.Register
{
    public class RegisterHandler(IAuthService authService) : IRequestHandler<Register, JwtTokenDto>
    {
        private readonly IAuthService authService = authService;

        public async Task<JwtTokenDto> Handle(Register request, CancellationToken cancellationToken)
        {
            var result = await authService.SignUp(request);
            return result;
        }
    }
}
