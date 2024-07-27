using Mentoroom.DOMAIN.Entities.LecturerModels;

namespace Mentoroom.DOMAIN.Interfaces
{
    public interface IAccessCodesRepository
    {
        Task<AccessCode> GenerateAccessCode(DateTime expirationDate);
        Task<List<AccessCode>> GetAccessCodes();
        Task<AccessCode> GetAccessCode(string accessCode);
        Task<AccessCode> DeactivateAccessCode(string codeId);
    }
}
