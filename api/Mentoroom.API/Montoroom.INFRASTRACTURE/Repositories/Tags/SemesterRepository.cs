using Mentoroom.DOMAIN.Entities.Tags;
using Mentoroom.DOMAIN.Interfaces.Tags;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.INFRASTRACTURE.Repositories.Tags
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly AppDbContext _dbContext;

        public SemesterRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Semester> AddSemester(Semester semester)
        {
            await _dbContext.Semesters.AddAsync(semester);
            await _dbContext.SaveChangesAsync();
            return semester;
        }

        public async Task<IEnumerable<Semester>> GetAllSemester()
        {
            var semesters = await _dbContext.Semesters.ToListAsync();
            return semesters;
        }
        public async Task<Semester> GetSemesterById(Guid id)
        {
            var semester = await _dbContext.Semesters.FirstOrDefaultAsync(d => d.Id == id);
            return semester;
        }

        public async Task<IEnumerable<Semester>> GetSemestersByYear(Guid id)
        {
            var semsters = await _dbContext.Semesters.Where(y => y.YearId == id).ToListAsync();
            return semsters;
        }
    }
}
