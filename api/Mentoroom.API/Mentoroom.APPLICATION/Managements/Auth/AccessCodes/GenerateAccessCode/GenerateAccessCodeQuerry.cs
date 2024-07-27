using MediatR;
using Mentoroom.DOMAIN.Entities.LecturerModels;

namespace Mentoroom.APPLICATION.Managements.Auth.AccessCodes.GenerateAccessCode
{
    public class GenerateAccessCodeQuerry : IRequest<AccessCode>
    {
        public DateTime ExpirationDate { get; set; }
    }
}
