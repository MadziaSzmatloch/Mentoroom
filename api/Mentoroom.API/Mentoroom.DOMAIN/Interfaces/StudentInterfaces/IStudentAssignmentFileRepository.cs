using Mentoroom.DOMAIN.Entities.StudentModels;

namespace Mentoroom.DOMAIN.Interfaces.StudentInterfaces
{
    public interface IStudentAssignmentFileRepository
    {
        Task<StudentAssignmentFile> SendFile(Guid studentAssignmentId, Guid assignmentFileId, string Uri);
        Task<StudentAssignmentFile> DeleteFile(Guid studentAssignmentFileId);
        Task AddStudentFile(StudentAssignmentFile studentAssignmentFile);
        Task<List<StudentAssignmentFile>> GetAllStudentAssignments();
    }
}
