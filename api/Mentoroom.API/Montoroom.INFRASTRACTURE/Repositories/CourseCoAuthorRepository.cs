using Mentoroom.DOMAIN.Entities.LecturerModels;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.INFRASTRACTURE.Repositories
{
    public class CourseCoAuthorRepository(AppDbContext dbContext, UserManager<AppUser> userManager) : ICourseCoAuthorRepository
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task<CourseCoAuthor> AddCourseCoAuthor(string authorId, Guid courseId)
        {
            var courseCoAuthor = new CourseCoAuthor()
            {
                CoAuthorId = authorId,
                CourseId = courseId
            };
            await dbContext.CourseCoAuthors.AddAsync(courseCoAuthor);
            await dbContext.SaveChangesAsync();
            return courseCoAuthor;

        }

        public async Task<CourseCoAuthor> DeleteCourseCoAuthor(CourseCoAuthor courseCoAuthor)
        {
            dbContext.CourseCoAuthors.Remove(courseCoAuthor);
            await dbContext.SaveChangesAsync();
            return courseCoAuthor;
        }

        public async Task<List<CourseCoAuthor>> GetCoAuthorsByCourseId(Guid courseId)
        {
            var coAuhtors = await dbContext.CourseCoAuthors.Where(cca => cca.CourseId == courseId).ToListAsync();
            return coAuhtors;
        }

        public async Task<List<CourseCoAuthor>> GetCoAuthors()
        {
            var coAuhtors = await dbContext.CourseCoAuthors.ToListAsync();
            return coAuhtors;
        }
    }
}
