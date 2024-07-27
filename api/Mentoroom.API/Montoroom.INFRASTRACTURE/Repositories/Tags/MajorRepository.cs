using Mentoroom.DOMAIN.Entities.Tags;
using Mentoroom.DOMAIN.Interfaces.Tags;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.INFRASTRACTURE.Repositories.Tags
{
    public class MajorRepository : IMajorRepository
    {
        private readonly AppDbContext _dbContext;

        public MajorRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Major> AddMajor(Major major)
        {
            await _dbContext.Majors.AddAsync(major);
            await _dbContext.SaveChangesAsync();
            return major;
        }

        public async Task<IEnumerable<Major>> GetAllMajors()
        {
            var majors = await _dbContext.Majors.ToListAsync();
            return majors;
        }

        public async Task<IEnumerable<Major>> GetMajorsByDepartment(Guid departmentId)
        {
            var majors = await _dbContext.Majors.Where(m => m.DepartmentId == departmentId).ToListAsync();
            return majors;
        }

        public async Task<Major> GetMajorById(Guid id)
        {
            var major = await _dbContext.Majors.FirstOrDefaultAsync(d => d.Id == id);
            return major;
        }
    }
}
