using MediatR;
using Mentoroom.DOMAIN.Entities.LecturerModels;

namespace Mentoroom.APPLICATION.Managements.Auth.AccessCodes.GetAccessCodes
{
    public class GetAccessCodesCommand : IRequest<List<AccessCode>>
    {
    }
}
