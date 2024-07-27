using Mentoroom.DOMAIN.Entities.Shared;

namespace Mentoroom.DOMAIN.Interfaces.Shared
{
    public interface IAssignmentRepository
    {
        Task<CourseAssignment> AddAssignment(CourseAssignment assignment);
        Task<IEnumerable<CourseAssignment>> GetAllAssignments();
        Task<IEnumerable<CourseAssignment>> GetAssignmentsByCourseId(Guid courseId);
        Task<CourseAssignment> DeleteAssignmentById(CourseAssignment assignment);
        Task<CourseAssignment> UpdateAssignment(CourseAssignment assignment);
        Task<CourseAssignment> GetAssignmentById(Guid assignmentId);
    }
}
