using Mentoroom.DOMAIN.Entities.Tags;
using Mentoroom.DOMAIN.Interfaces.Tags;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.INFRASTRACTURE.Repositories.Tags
{
    public class YearRepository : IYearRepository
    {
        private readonly AppDbContext _dbContext;

        public YearRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Year> AddYear(Year year)
        {
            await _dbContext.Years.AddAsync(year);
            await _dbContext.SaveChangesAsync();
            return year;
        }

        public async Task<IEnumerable<Year>> GetAllYear()
        {
            var years = await _dbContext.Years.Include(y => y.Semesters).ToListAsync();
            return years;
        }

        public async Task<Year> GetYearById(Guid id)
        {
            var year = await _dbContext.Years.Include(y => y.Semesters).FirstOrDefaultAsync(d => d.Id == id);
            return year;
        }

        public async Task<IEnumerable<Year>> GetYearsByDegree(Guid id)
        {
            var years = await _dbContext.Years.Include(y => y.Semesters).Where(y => y.DegreeId == id).ToListAsync();
            return years;
        }
    }
}
