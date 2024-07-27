using MediatR;
using Mentoroom.DOMAIN.Entities.LecturerModels;
using Mentoroom.DOMAIN.Interfaces;

namespace Mentoroom.APPLICATION.Managements.Auth.AccessCodes.GetAccessCodes
{
    internal class GetAccessCodesCommandHandler(IAccessCodesRepository accessCodesRepositoryRepository) : IRequestHandler<GetAccessCodesCommand, List<AccessCode>>
    {
        public async Task<List<AccessCode>> Handle(GetAccessCodesCommand request, CancellationToken cancellationToken)
        {
            var accessCodes = await accessCodesRepositoryRepository.GetAccessCodes();
            return accessCodes;
        }
    }
}
