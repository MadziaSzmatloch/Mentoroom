using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.INFRASTRACTURE.Repositories.Shared
{
    public class AssignmentRepository(AppDbContext dbContext, IAssignmentFileRepository assignmentFileRepository) : IAssignmentRepository
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly IAssignmentFileRepository assignmentFileRepository = assignmentFileRepository;

        public async Task<CourseAssignment> AddAssignment(CourseAssignment assignment)
        {
            await _dbContext.Assignments.AddAsync(assignment);
            await _dbContext.SaveChangesAsync();
            return assignment;
        }

        public async Task<CourseAssignment> DeleteAssignmentById(CourseAssignment assignment)
        {
            _dbContext.Assignments.Remove(assignment);
            await _dbContext.SaveChangesAsync();
            return assignment;
        }

        public async Task<IEnumerable<CourseAssignment>> GetAllAssignments()
        {
            var assignments = await _dbContext.Assignments
                .Include(a => a.Course).ThenInclude(c => c.Tags)
                .Include(a => a.Course).ThenInclude(c => c.Author)
                .Include(a => a.Course).ThenInclude(c => c.CoAuthors)
                .Include(a => a.Course).ThenInclude(c => c.Students)
                .Include(a => a.StudentAssignments)
                .Include(a => a.AssigmnentAttachments)
                .Include(a => a.RequiredFiles)
                .ToListAsync();
            return assignments;
        }

        public async Task<IEnumerable<CourseAssignment>> GetAssignmentsByCourseId(Guid courseId)
        {
            var assignments = await _dbContext.Assignments
                .Include(a => a.Course).ThenInclude(c => c.Tags)
                .Include(a => a.Course).ThenInclude(c => c.Author)
                .Include(a => a.Course).ThenInclude(c => c.CoAuthors)
                .Include(a => a.Course).ThenInclude(c => c.Students)
                .Include(a => a.StudentAssignments)
                .Include(a => a.AssigmnentAttachments)
                .Include(a => a.RequiredFiles)
                .Where(a => a.CourseId == courseId)
                .ToListAsync();
            return assignments;
        }

        public async Task<CourseAssignment> UpdateAssignment(CourseAssignment request)
        {
            var assignment = await _dbContext.Assignments
                .Include(a => a.RequiredFiles)
                .FirstOrDefaultAsync(a => a.Id == request.Id);

            assignment.Name = request.Name;
            assignment.Description = request.Description;
            assignment.IsActive = request.IsActive;
            assignment.DeadlineDate = request.DeadlineDate;

            await _dbContext.SaveChangesAsync();

            return assignment;
        }

        public async Task<CourseAssignment> GetAssignmentById(Guid assignmentId)
        {
            var assignment = await _dbContext.Assignments
                .Include(a => a.Course).ThenInclude(c => c.Tags)
                .Include(a => a.Course).ThenInclude(c => c.Author)
                .Include(a => a.Course).ThenInclude(c => c.CoAuthors)
                .Include(a => a.Course).ThenInclude(c => c.Students)
                .Include(a => a.StudentAssignments)
                .Include(a => a.AssigmnentAttachments)
                .Include(a => a.RequiredFiles)
                .FirstOrDefaultAsync(a => a.Id == assignmentId);
            return assignment;
        }
    }
}
