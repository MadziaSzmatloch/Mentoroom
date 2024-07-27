using Mentoroom.DOMAIN.Entities.LecturerModels;
using Mentoroom.INFRASTRACTURE.Persistence;

namespace Mentoroom.INFRASTRACTURE.Seeders.Authors
{
    public class LecturerSeeder(AppDbContext dbContext)
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task Seed()
        {
            //if (await dbContext.Database.CanConnectAsync())
            //{
            //    if (!dbContext.Lecturers.Any())
            //    {
            //        var user = dbContext.Users.First(u => u.FirstName == "Lecturer");

            //        var lecturer = new Lecturer() { UserId = user.Id, User = user };

            //        dbContext.Lecturers.Add(lecturer);
            //        await dbContext.SaveChangesAsync();
            //    }
            //}

            await SeedAccessCodes();
        }

        public async Task SeedAccessCodes()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.AccessCodes.Any())
                {
                    for (int i = 0; i < 10; i++)
                    {
                        var accessCode = new AccessCode(DateTime.Now.AddDays(i + 1));
                        await accessCode.GenerateUniqueCode(dbContext);

                        dbContext.AccessCodes.Add(accessCode);
                    }

                    var inActiveAccessCode = new AccessCode(DateTime.Now.AddDays(0));
                    await inActiveAccessCode.GenerateUniqueCode(dbContext);
                    inActiveAccessCode.IsActive = false;

                    dbContext.AccessCodes.Add(inActiveAccessCode);

                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}

