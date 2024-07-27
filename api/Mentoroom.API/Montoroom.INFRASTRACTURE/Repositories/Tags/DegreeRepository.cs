using Mentoroom.DOMAIN.Entities.Tags;
using Mentoroom.DOMAIN.Interfaces.Tags;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.INFRASTRACTURE.Repositories.Tags
{
    public class DegreeRepository : IDegreeRepository
    {
        private readonly AppDbContext _dbContext;

        public DegreeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Degree> AddDegree(Degree degree)
        {
            await _dbContext.Degrees.AddAsync(degree);
            await _dbContext.SaveChangesAsync();
            return degree;
        }

        public async Task<IEnumerable<Degree>> GetAllDegree()
        {
            var degrees = await _dbContext.Degrees.Include(d => d.Years).ThenInclude(y => y.Semesters).ToListAsync();
            return degrees;
        }

        public async Task<Degree> GetDegreeById(Guid id)
        {
            var degree = await _dbContext.Degrees.Include(d => d.Years).ThenInclude(y => y.Semesters).FirstOrDefaultAsync(d => d.Id == id);
            return degree;
        }
    }
}
