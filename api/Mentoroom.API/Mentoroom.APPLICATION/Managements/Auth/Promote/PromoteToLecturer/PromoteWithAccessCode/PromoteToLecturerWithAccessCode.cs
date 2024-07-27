using MediatR;
using Mentoroom.APPLICATION.Services.AuthServices;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.DOMAIN.Models;

namespace Mentoroom.APPLICATION.Managements.Auth.Promote.PromoteToLecturer.PromoteWithAccessCode
{
    public class PromoteToLecturerWithAccessCode : IRequest
    {
        public string UserID { get; set; }
        public string AccessCode { get; set; }
    }


    public class PromoteToLecturerWithAccessCodeHandler(IAuthService authService, IAccessCodesRepository accessCodesRepository) : IRequestHandler<PromoteToLecturerWithAccessCode>
    {
        private readonly IAuthService authService = authService;
        private readonly IAccessCodesRepository accessCodesRepository = accessCodesRepository;

        public async Task Handle(PromoteToLecturerWithAccessCode request, CancellationToken cancellationToken)
        {
            var code = await accessCodesRepository.GetAccessCode(request.AccessCode);

            if (!code.IsActive || code.ExpirationDate < DateTime.Now)
            {
                await accessCodesRepository.DeactivateAccessCode(code.Id.ToString());
                throw new Exception("The access code has expired. Contact the administrator to obtain a new access code");
            }
            else
            {
                var result = await authService.PromoteTo(UserRoles.Lecturer, request.UserID);
                if (!result)
                {
                    throw new Exception("Something went wrong!");
                }
            }
        }
    }
}
