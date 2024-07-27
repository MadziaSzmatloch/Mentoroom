using MediatR;
using Mentoroom.DOMAIN.Entities.LecturerModels;
using Mentoroom.DOMAIN.Interfaces;

namespace Mentoroom.APPLICATION.Managements.Auth.AccessCodes.GenerateAccessCode
{
    internal class GenerateAccessCodeQuerryHandler(IAccessCodesRepository accessCodesRepositoryRepository) : IRequestHandler<GenerateAccessCodeQuerry, AccessCode>
    {
        public async Task<AccessCode> Handle(GenerateAccessCodeQuerry request, CancellationToken cancellationToken)
        {
            return await accessCodesRepositoryRepository.GenerateAccessCode(request.ExpirationDate);
        }
    }
}
