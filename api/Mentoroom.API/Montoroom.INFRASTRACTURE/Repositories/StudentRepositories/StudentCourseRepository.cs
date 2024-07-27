using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Entities.StudentModels;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.INFRASTRACTURE.Repositories.StudentRepositories
{
    public class StudentCourseRepository(AppDbContext dbContext, UserManager<AppUser> userManager) : IStudentCourseRepository
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task<StudentCourse> AddStudentCourse(StudentCourse studentCourse)
        {
            await dbContext.StudentCourses.AddAsync(studentCourse);
            await dbContext.SaveChangesAsync();
            return studentCourse;
        }

        public async Task<IEnumerable<StudentCourse>> GetStudentCoursesByCourseId(Guid courseId)
        {
            var studentCourses = await dbContext.StudentCourses
                .Include(x => x.Course)
                .ThenInclude(c => c.Tags)
                .Include(x => x.Student)
                .Where(x => x.CourseId == courseId)
                .ToListAsync();
            return studentCourses;
        }

        public async Task<IEnumerable<StudentCourse>> GetStudentCoursesByStudentId(string studentId)
        {
            var studentCourses = await dbContext.StudentCourses
                .Include(x => x.Course)
                .ThenInclude(c => c.Tags)
                .Include(x => x.Student)
                .Where(x => x.StudentId == studentId)
                .ToListAsync();
            return studentCourses;
        }

        public async Task<IEnumerable<StudentCourse>> GetAllStudentCourses()
        {
            var studentCourses = await dbContext.StudentCourses
                .Include(x => x.Course)
                .ThenInclude(c => c.Tags)
                .Include(x => x.Student)
                .ToListAsync();
            return studentCourses;
        }

        public async Task<StudentCourse> DeleteStudentCourse(StudentCourse studentCourse)
        {
            dbContext.StudentCourses.Remove(studentCourse);
            await dbContext.SaveChangesAsync();
            return studentCourse;
        }

        public async Task<StudentCourse> ConfirmStudent(string studentId, Guid courseId)
        {
            var studentCourse = await dbContext.StudentCourses
                        .Include(x => x.Course)
                        .ThenInclude(c => c.Tags)
                        .Include(x => x.Student)
                        .FirstOrDefaultAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId);
            studentCourse!.IsConfirmed = true;
            await dbContext.SaveChangesAsync();
            return studentCourse;
        }

        public async Task<StudentCourse> RemoveStudentCourse(string studentId, Guid courseId)
        {
            var studentCourse = await dbContext.StudentCourses
                        .Include(x => x.Course)
                        .ThenInclude(c => c.Tags)
                        .Include(x => x.Student)
                        .FirstOrDefaultAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId);

            studentCourse!.IsConfirmed = false;
            await dbContext.SaveChangesAsync();
            return studentCourse;
        }
    }
}
