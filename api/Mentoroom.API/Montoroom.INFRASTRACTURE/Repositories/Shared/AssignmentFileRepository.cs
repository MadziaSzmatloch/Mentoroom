using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.INFRASTRACTURE.Repositories.Shared
{
    public class AssignmentFileRepository(AppDbContext dbContext) : IAssignmentFileRepository
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task<AssignmentFile> AddFile(AssignmentFile assignmentFile)
        {
            await dbContext.AssignmentFiles.AddAsync(assignmentFile);
            await dbContext.SaveChangesAsync();

            return assignmentFile;
        }

        public async Task DeleteFile(AssignmentFile assignmentFile)
        {
            dbContext.AssignmentFiles.Remove(assignmentFile);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AssignmentFile>> GetAllAssignmentFiles()
        {
            var assignmentFiles = await dbContext.AssignmentFiles.Include(f => f.Assignment).ToListAsync();
            return assignmentFiles;
        }

        public async Task<AssignmentFile> UpdateFile(AssignmentFile request)
        {
            var assignmentFile = await dbContext.AssignmentFiles.Include(f => f.Assignment).FirstOrDefaultAsync(f => f.Id == request.Id);

            assignmentFile.Extension = request.Extension;
            assignmentFile.MaxSizeInKb = request.MaxSizeInKb;
            assignmentFile.FileNameSuffix = request.FileNameSuffix;
            await dbContext.SaveChangesAsync();

            return assignmentFile;
        }
    }
}
