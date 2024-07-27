using MediatR;
using Mentoroom.DOMAIN.Entities.LecturerModels;
using Mentoroom.DOMAIN.Interfaces;

namespace Mentoroom.APPLICATION.Managements.Auth.AccessCodes.DeactivateAccessCode
{
    public class DeactivateAccessCode : IRequest<AccessCode>
    {
        public string CodeID { get; set; }
    }

    internal class DeactivateAccessCodeHandler(IAccessCodesRepository accessCodesRepository) : IRequestHandler<DeactivateAccessCode, AccessCode>
    {
        private readonly IAccessCodesRepository accessCodesRepository = accessCodesRepository;

        public async Task<AccessCode> Handle(DeactivateAccessCode request, CancellationToken cancellationToken)
        {
            var code = await accessCodesRepository.DeactivateAccessCode(request.CodeID);
            return code;
        }
    }
}
