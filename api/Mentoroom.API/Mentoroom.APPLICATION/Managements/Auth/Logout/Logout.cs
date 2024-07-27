using MediatR;
using System.Security.Claims;

namespace Mentoroom.APPLICATION.Managements.Auth.Logout
{
    public class Logout : IRequest<bool>
    {
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
