using Mentoroom.DOMAIN.Entities.StudentModels;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.INFRASTRACTURE.Seeders.StudentSeeders
{
    public class StudentAssignmentFilesSeeder(AppDbContext dbContext, IFileRepository fileRepository)
    {
        private readonly AppDbContext dbContext = dbContext;
        private readonly IFileRepository fileRepository = fileRepository;

        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.StudentAssignmentFiles.Any())
                {
                    var studentFiles = (await fileRepository.BlobList()).Where(f => f.Name.StartsWith("studentfiles"));
                    foreach (var studentFile in studentFiles)
                    {
                        await fileRepository.Delete(studentFile.Name);
                    }

                    var studentassignments = await dbContext.StudentAssignments
                        .Include(sa => sa.CourseAssignment).ThenInclude(ca => ca.RequiredFiles)
                        .ToListAsync();

                    var studentAssignmentFiles = new List<StudentAssignmentFile>();
                    foreach (var studentassignment in studentassignments)
                    {
                        foreach (var requiredFile in studentassignment.CourseAssignment.RequiredFiles)
                        {
                            studentAssignmentFiles.Add(new StudentAssignmentFile()
                            {
                                IsSended = false,
                                StudentAssignmentId = studentassignment.Id,
                                AssignmentFileId = requiredFile.Id,
                            });
                        }
                    }

                    foreach (var studentAssignmentFile in studentAssignmentFiles)
                    {
                        dbContext.StudentAssignmentFiles.Add(studentAssignmentFile);
                    }

                    await dbContext.SaveChangesAsync();

                }
            }
        }
    }
}
