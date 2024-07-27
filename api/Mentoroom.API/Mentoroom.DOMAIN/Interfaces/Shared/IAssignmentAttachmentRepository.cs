using Mentoroom.DOMAIN.Entities.Shared;

namespace Mentoroom.DOMAIN.Interfaces.Shared
{
    public interface IAssignmentAttachmentRepository
    {
        Task<AssignmentAttachment> AddAttachment(AssignmentAttachment attachment);
        Task<IEnumerable<AssignmentAttachment>> GetAllAttachments();
        Task DeleteAttachment(AssignmentAttachment attachment);
    }
}
