using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.INFRASTRACTURE.Persistence;

namespace Mentoroom.INFRASTRACTURE.Seeders.Shared.Assignments
{
    public class AssignmentFilesSeeder(AppDbContext dbContext)
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task Seed()
        {

            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.AssignmentFiles.Any())
                {
                    var assignment1 = dbContext.Assignments.First(a => a.Name == "Assignment 1");
                    var assignment2 = dbContext.Assignments.First(a => a.Name == "Assignment 2");
                    var assignment3 = dbContext.Assignments.First(a => a.Name == "Assignment 3");


                    var file1 = new AssignmentFile()
                    {
                        Extension = "pdf",
                        MaxSizeInKb = 200,
                        FileNameSuffix = $"dokumentacja",
                        AssignmentId = assignment1.Id
                    };
                    var file2 = new AssignmentFile()
                    {
                        Extension = "png",
                        MaxSizeInKb = 400,
                        FileNameSuffix = $"obraz",
                        AssignmentId = assignment1.Id
                    };
                    var file3 = new AssignmentFile()
                    {
                        Extension = "png",
                        MaxSizeInKb = 200,
                        FileNameSuffix = $"obraz",
                        AssignmentId = assignment2.Id
                    };


                    dbContext.AssignmentFiles.Add(file1);
                    dbContext.AssignmentFiles.Add(file2);
                    dbContext.AssignmentFiles.Add(file3);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
