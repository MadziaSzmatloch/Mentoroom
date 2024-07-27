using Mentoroom.DOMAIN.Entities.Shared;

namespace Mentoroom.DOMAIN.Interfaces.Shared
{
    public interface IAssignmentFileRepository
    {
        Task<AssignmentFile> AddFile(AssignmentFile assignmentFile);
        Task<IEnumerable<AssignmentFile>> GetAllAssignmentFiles();
        Task DeleteFile(AssignmentFile assignmentFile);
        Task<AssignmentFile> UpdateFile(AssignmentFile assignmentFile);
    }
}
