using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.INFRASTRACTURE.Repositories.Shared
{
    public class AssignmentAttachmentRepository(AppDbContext dbContext) : IAssignmentAttachmentRepository
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task<AssignmentAttachment> AddAttachment(AssignmentAttachment attachment)
        {
            await dbContext.AssigmnentAttachments.AddAsync(attachment);
            await dbContext.SaveChangesAsync();
            return attachment;
        }

        public async Task DeleteAttachment(AssignmentAttachment attachment)
        {
            dbContext.AssigmnentAttachments.Remove(attachment);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AssignmentAttachment>> GetAllAttachments()
        {
            var attachments = await dbContext.AssigmnentAttachments.Include(a => a.Assignment).ToListAsync();
            return attachments;

        }
    }
}
