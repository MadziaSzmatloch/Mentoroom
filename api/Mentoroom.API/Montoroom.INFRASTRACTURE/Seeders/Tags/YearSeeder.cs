using Mentoroom.DOMAIN.Entities.Tags;
using Mentoroom.INFRASTRACTURE.Persistence;

namespace Mentoroom.INFRASTRACTURE.Seeders.Tags
{
    public class YearSeeder(AppDbContext dbContext)
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Years.Any())
                {
                    var bachelorDegree = dbContext.Degrees.First(d => d.Name == "Bachelor's");
                    var mastersDegree = dbContext.Degrees.First(d => d.Name == "Master's");
                    var phdDegree = dbContext.Degrees.First(d => d.Name == "Doctorate");
                    var year1 = new Year() { Name = "1", DegreeId = bachelorDegree.Id };
                    var year2 = new Year() { Name = "2", DegreeId = bachelorDegree.Id };
                    var year3 = new Year() { Name = "3", DegreeId = bachelorDegree.Id };

                    var year4 = new Year() { Name = "1", DegreeId = mastersDegree.Id };
                    var year5 = new Year() { Name = "2", DegreeId = mastersDegree.Id };
                    var year6 = new Year() { Name = "3", DegreeId = mastersDegree.Id };

                    var year7 = new Year() { Name = "1", DegreeId = phdDegree.Id };
                    var year8 = new Year() { Name = "2", DegreeId = phdDegree.Id };
                    var year9 = new Year() { Name = "3", DegreeId = phdDegree.Id };

                    dbContext.Years.Add(year1);
                    dbContext.Years.Add(year2);
                    dbContext.Years.Add(year3);
                    dbContext.Years.Add(year4);
                    dbContext.Years.Add(year5);
                    dbContext.Years.Add(year6);
                    dbContext.Years.Add(year7);
                    dbContext.Years.Add(year8);
                    dbContext.Years.Add(year9);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
