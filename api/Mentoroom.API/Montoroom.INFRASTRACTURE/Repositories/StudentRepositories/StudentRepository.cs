using Mentoroom.DOMAIN.Entities.StudentModels;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.INFRASTRACTURE.Repositories.StudentRepositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _dbContext;

        public StudentRepository(AppDbContext context)
        {
            _dbContext = context;
        }
        public async Task<Student> AddStudent(Student student)
        {
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return student;
        }

        public async Task RemoveStudentByUserId(string userId)
        {
            var student = await _dbContext.Students.FirstOrDefaultAsync(s => s.UserId == userId);

            if (student == null)
            {
                throw new Exception("Student with given id doesnt exist");
            }

            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            var students = await _dbContext.Students.ToListAsync();
            return students;
        }

        public async Task<Student> GetStudentById(string studentId)
        {
            var student = await _dbContext.Students
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.UserId == studentId);

            return student == null ? throw new Exception("Student with given id doesnt exist") : student;
        }
    }
}
