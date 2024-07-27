using Mentoroom.DOMAIN.Entities.LecturerModels;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Mentoroom.INFRASTRACTURE.Repositories
{
    public class AccessCodesRepository(AppDbContext context, UserManager<AppUser> userManager) : IAccessCodesRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<AccessCode> GenerateAccessCode(DateTime expirationDate)
        {
            var accessCode = new AccessCode(expirationDate);
            await accessCode.GenerateUniqueCode(_context);

            _context.AccessCodes.Add(accessCode);
            _context.SaveChanges();
            return accessCode;
        }

        public async Task<List<AccessCode>> GetAccessCodes()
        {
            return await _context.AccessCodes
                .OrderByDescending(ac => ac.CreationDate)
                .OrderByDescending(ac => ac.IsActive)
                .ToListAsync();
        }

        public async Task<AccessCode> GetAccessCode(string accessCode)
        {
            var code = await _context.AccessCodes.FirstOrDefaultAsync(x => x.Code == accessCode);
            if (code == null)
            {
                throw new Exception("Access code does not exist!");
            }
            return code;
        }

        public async Task<AccessCode> DeactivateAccessCode(string codeId)
        {
            var code = await _context.AccessCodes.FirstOrDefaultAsync(x => x.Id.ToString() == codeId);
            if (code == null)
            {
                throw new Exception("Access code does not exist!");
            }

            code.IsActive = false;
            await _context.SaveChangesAsync();

            return code;
        }
    }
}
