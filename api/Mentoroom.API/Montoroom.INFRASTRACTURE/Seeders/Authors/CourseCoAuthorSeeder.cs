using Mentoroom.DOMAIN.Entities.LecturerModels;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Models;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Mentoroom.INFRASTRACTURE.Seeders.Authors
{
    public class CourseCoAuthorSeeder(AppDbContext dbContext, UserManager<AppUser> userManager)
    {
        private readonly AppDbContext dbContext = dbContext;
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.CourseCoAuthors.Any())
                {
                    var lecturers = await userManager.GetUsersInRoleAsync(UserRoles.Lecturer);

                    var course1 = dbContext.Courses.First(d => d.Name == "Programowanie");
                    var course2 = dbContext.Courses.First(d => d.Name == "Inżynieria budowlana");
                    var course3 = dbContext.Courses.First(d => d.Name == "Teoria obwodów");
                    var course4 = dbContext.Courses.First(d => d.Name == "Pomiary elektryczne w przemyśle");

                    var coAuthors = new List<CourseCoAuthor>
                    {
                        new CourseCoAuthor() { Course = course1, CoAuthor = lecturers[1] },
                        new CourseCoAuthor() { Course = course1, CoAuthor = lecturers[2] },
                        new CourseCoAuthor() { Course = course2, CoAuthor = lecturers[3] },
                        new CourseCoAuthor() { Course = course2, CoAuthor = lecturers[4] },
                        new CourseCoAuthor() { Course = course3, CoAuthor = lecturers[4] },
                        new CourseCoAuthor() { Course = course3, CoAuthor = lecturers[5] },
                        new CourseCoAuthor() { Course = course4, CoAuthor = lecturers[5] },
                        new CourseCoAuthor() { Course = course4, CoAuthor = lecturers[6] }
                    };

                    dbContext.CourseCoAuthors.AddRange(coAuthors);


                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
