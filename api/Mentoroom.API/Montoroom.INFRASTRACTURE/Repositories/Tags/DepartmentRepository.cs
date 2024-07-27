using Mentoroom.DOMAIN.Entities.Tags;
using Mentoroom.DOMAIN.Interfaces.Tags;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.INFRASTRACTURE.Repositories.Tags
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _dbContext;

        public DepartmentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Department> AddDepartment(Department department)
        {
            await _dbContext.Departments.AddAsync(department);
            await _dbContext.SaveChangesAsync();
            return department;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            var departments = await _dbContext.Departments.Include(d => d.Majors).ToListAsync();
            return departments;
        }
        public async Task<Department> GetDepartmentById(Guid id)
        {
            var department = await _dbContext.Departments.Include(d => d.Majors).FirstOrDefaultAsync(d => d.Id == id);
            return department;
        }
    }
}
