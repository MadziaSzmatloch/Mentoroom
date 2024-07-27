using Mentoroom.DOMAIN.Entities.Tags;
using Mentoroom.INFRASTRACTURE.Persistence;

namespace Mentoroom.INFRASTRACTURE.Seeders.Tags
{
    public class DegreeSeeder(AppDbContext dbContext)
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Degrees.Any())
                {
                    var bachelor = new Degree() { Name = "Bachelor's" };
                    var masters = new Degree() { Name = "Master's" };
                    var phd = new Degree() { Name = "Doctorate" };

                    dbContext.Degrees.Add(bachelor);
                    dbContext.Degrees.Add(masters);
                    dbContext.Degrees.Add(phd);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
