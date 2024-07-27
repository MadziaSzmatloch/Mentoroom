using Mentoroom.DOMAIN.Entities.StudentModels;
using Mentoroom.INFRASTRACTURE.Persistence;

namespace Mentoroom.INFRASTRACTURE.Seeders.StudentSeeders
{
    public class StudentSeeder(AppDbContext dbContext)
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Students.Any())
                {
                    var user = dbContext.Users.First(u => u.FirstName == "Student");

                    var student = new Student() { IndexNumber = "st732", UserId = user.Id, User = user };

                    dbContext.Students.Add(student);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
