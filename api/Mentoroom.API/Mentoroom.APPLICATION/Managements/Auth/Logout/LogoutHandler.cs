using MediatR;
using Mentoroom.APPLICATION.Services.AuthServices;

namespace Mentoroom.APPLICATION.Managements.Auth.Logout
{
    public class LogoutHandler(IAuthService authService) : IRequestHandler<Logout, bool>
    {
        private readonly IAuthService authService = authService;

        public async Task<bool> Handle(Logout request, CancellationToken cancellationToken)
        {
            var result = await authService.Logout(request);
            return result;
        }
    }
}
