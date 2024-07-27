using Mentoroom.DOMAIN.Entities.StudentModels;

namespace Mentoroom.DOMAIN.Interfaces.StudentInterfaces
{
    public interface IStudentAssignmentRepository
    {
        Task<StudentAssignment> AddStudentAssignmnent(StudentAssignment studentAssignment);
        Task<List<StudentAssignment>> GetAllStudentAssignments();
        Task<StudentAssignment> ChangeCompletedToTrue(Guid studentAssignmentId);
    }
}
