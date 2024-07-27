using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.INFRASTRACTURE.Repositories.Shared
{
    public class CourseRepository(AppDbContext dbContext, UserManager<AppUser> userManager) : ICourseRepository
    {
        private readonly AppDbContext dbContext = dbContext;
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task<Course> AddCourse(Course course)
        {
            await dbContext.Courses.AddAsync(course);
            await dbContext.SaveChangesAsync();
            return course;
        }

        public async Task<Course> DeleteCourseById(Course course)
        {
            dbContext.Courses.Remove(course);
            await dbContext.SaveChangesAsync();
            return course;
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            var courses = await dbContext.Courses
                .Include(c => c.Tags)
                .Include(c => c.Assignments)
                .Include(c => c.Students).ThenInclude(s => s.Student).ThenInclude(s => s.User)
                .Include(c => c.Author)
                .Include(c => c.CoAuthors).ThenInclude(ca => ca.CoAuthor)
                .ToListAsync();
            return courses;
        }

        public async Task<Course> GetCourseById(Guid courseId)
        {
            var course = await dbContext.Courses
                .Include(c => c.Tags)
                .Include(c => c.Assignments).ThenInclude(a => a.RequiredFiles)
                .Include(c => c.Students).ThenInclude(s => s.Student).ThenInclude(s => s.User)
                .Include(c => c.Author)
                .Include(c => c.CoAuthors).ThenInclude(cca => cca.CoAuthor)
                .FirstOrDefaultAsync(c => c.Id == courseId);
            return course;
        }

        public async Task<IEnumerable<Course>> GetCoursesByTeacher(string userId)
        {
            var allCourses = await dbContext.Courses
                .Include(c => c.Tags)
                .Include(c => c.Assignments)
                .Include(c => c.Students).ThenInclude(s => s.Student).ThenInclude(s => s.User)
                .Include(c => c.Author)
                .Include(c => c.CoAuthors).ThenInclude(cca => cca.CoAuthor)
                .ToListAsync();

            var courseAsAuthor = allCourses.Where(c => c.AuthorId == userId);
            var coursesAsCoAuthor = allCourses.Where(c => c.CoAuthors.Any(cca => cca.CoAuthorId == userId));
            var result = courseAsAuthor.Union(coursesAsCoAuthor);
            return result;
        }

        public async Task<Course> UpdateCourse(Course request)
        {
            var course = await dbContext.Courses
                .Include(c => c.Tags)
                .Include(c => c.Assignments)
                .Include(c => c.Students).ThenInclude(s => s.Student).ThenInclude(s => s.User)
                .Include(c => c.Author)
                .Include(c => c.CoAuthors).ThenInclude(cca => cca.CoAuthor)
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            course.Name = request.Name;
            course.Description = request.Description;
            course.IsActive = request.IsActive;
            course.Tags.Degree = request.Tags.Degree;
            course.Tags.Year = request.Tags.Year;
            course.Tags.Semester = request.Tags.Semester;
            course.Tags.Department = request.Tags.Department;
            course.Tags.ShortDepartment = request.Tags.ShortDepartment;
            course.Tags.Major = request.Tags.Major;

            await dbContext.SaveChangesAsync();
            return course;
        }
    }
}
