using MediatR;
using Mentoroom.APPLICATION.Services.AuthServices.JwtToken;

namespace Mentoroom.APPLICATION.Managements.Auth.RefreshToken
{
    public class RefreshToken : JwtTokenDto, IRequest<JwtTokenDto>
    {
    }
}
