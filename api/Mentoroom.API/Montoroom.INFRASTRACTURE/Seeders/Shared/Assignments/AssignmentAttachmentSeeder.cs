using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.INFRASTRACTURE.Seeders.Shared.Assignments
{
    public class AssignmentAttachmentSeeder(AppDbContext dbContext, IFileRepository fileRepository)
    {
        private readonly AppDbContext dbContext = dbContext;
        private readonly IFileRepository fileRepository = fileRepository;

        public async Task Seed()
        {

            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.AssigmnentAttachments.Any())
                {
                    var assignmentFiles = (await fileRepository.BlobList()).Where(f => f.Name.StartsWith("assignmentfiles"));
                    foreach (var assignmentFile in assignmentFiles)
                    {
                        await fileRepository.Delete(assignmentFile.Name);
                    }

                    var assignment1 = dbContext.Assignments.Include(a => a.Course).First(a => a.Name == "Assignment 1");
                    var assignment2 = dbContext.Assignments.Include(a => a.Course).First(a => a.Name == "Assignment 2");
                    var assignment3 = dbContext.Assignments.Include(a => a.Course).First(a => a.Name == "Assignment 3");
                    var assignment5 = dbContext.Assignments.Include(a => a.Course).First(a => a.Name == "Assignment 5");

                    var file1 = await fileRepository.UploadFile(Path.Combine(Directory.GetCurrentDirectory(), "Assets/tekst.txt"), $"assignmentfiles/{assignment1.Course.Name}/{assignment1.Name}/plik1");
                    var file2 = await fileRepository.UploadFile(Path.Combine(Directory.GetCurrentDirectory(), "Assets/prezka.pptx"), $"assignmentfiles/{assignment2.Course.Name}/{assignment2.Name}/plik2");
                    var file3 = await fileRepository.UploadFile(Path.Combine(Directory.GetCurrentDirectory(), "Assets/Obraz3.jpg"), $"assignmentfiles/{assignment3.Course.Name}/{assignment3.Name}/plik3");
                    var file4 = await fileRepository.UploadFile(Path.Combine(Directory.GetCurrentDirectory(), "Assets/Zadanie długie.pdf"), $"assignmentfiles/{assignment5.Course.Name}/{assignment5.Name}/plik4");
                    var file5 = await fileRepository.UploadFile(Path.Combine(Directory.GetCurrentDirectory(), "Assets/Zadanie długie.pdf"), $"assignmentfiles/{assignment5.Course.Name}/{assignment1.Name}/plik5");

                    var attachment1 = new AssignmentAttachment()
                    {
                        Uri = file1.Uri,
                        Name = "plik1.txt",
                        ContentType = file1.ContentType,
                        AssignmentId = assignment1.Id,
                    };
                    var attachment2 = new AssignmentAttachment()
                    {
                        Uri = file2.Uri,
                        Name = "plik2.pptx",
                        ContentType = file2.ContentType,
                        AssignmentId = assignment2.Id,
                    };

                    var attachment3 = new AssignmentAttachment()
                    {
                        Uri = file3.Uri,
                        Name = "plik3.jpg",
                        ContentType = file3.ContentType,
                        AssignmentId = assignment3.Id,
                    };
                    var attachment4 = new AssignmentAttachment()
                    {
                        Uri = file4.Uri,
                        Name = "plik4.pdf",
                        ContentType = file4.ContentType,
                        AssignmentId = assignment5.Id,
                    };
                    var attachment6 = new AssignmentAttachment()
                    {
                        Uri = file5.Uri,
                        Name = "plik5.pdf",
                        ContentType = file5.ContentType,
                        AssignmentId = assignment1.Id,
                    };

                    dbContext.AssigmnentAttachments.Add(attachment1);
                    dbContext.AssigmnentAttachments.Add(attachment2);
                    dbContext.AssigmnentAttachments.Add(attachment3);
                    dbContext.AssigmnentAttachments.Add(attachment4);
                    dbContext.AssigmnentAttachments.Add(attachment6);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
