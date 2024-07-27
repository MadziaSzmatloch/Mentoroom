using Mentoroom.DOMAIN.Entities.StudentModels;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.INFRASTRACTURE.Repositories.StudentRepositories
{
    public class StudentAssignmentRepository(AppDbContext dbContext) : IStudentAssignmentRepository
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task<StudentAssignment> AddStudentAssignmnent(StudentAssignment studentAssignment)
        {
            await dbContext.StudentAssignments.AddAsync(studentAssignment);
            await dbContext.SaveChangesAsync();
            return studentAssignment;

        }

        public async Task<List<StudentAssignment>> GetAllStudentAssignments()
        {
            var studentAssignments = await dbContext.StudentAssignments
                .Include(sa => sa.StudentCourse).ThenInclude(sc => sc.Student)
                .Include(sa => sa.StudentCourse).ThenInclude(sc => sc.Course)
                .Include(sa => sa.CourseAssignment).ThenInclude(ca => ca.Course)
                .Include(sa => sa.CourseAssignment).ThenInclude(ca => ca.AssigmnentAttachments)
                .Include(sa => sa.StudentFiles)
                .ToListAsync();
            return studentAssignments;
        }

        public async Task<StudentAssignment> ChangeCompletedToTrue(Guid studentAssignmentId)
        {
            var studentAssignment = await dbContext.StudentAssignments.FirstOrDefaultAsync(x => x.Id == studentAssignmentId);
            studentAssignment.IsCompleted = true;

            await dbContext.SaveChangesAsync();
            return studentAssignment;
        }
    }
}
