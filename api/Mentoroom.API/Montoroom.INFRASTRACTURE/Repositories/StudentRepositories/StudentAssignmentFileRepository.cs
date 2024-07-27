using Mentoroom.DOMAIN.Entities.StudentModels;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.INFRASTRACTURE.Repositories.StudentRepositories
{
    public class StudentAssignmentFileRepository(AppDbContext dbContext) : IStudentAssignmentFileRepository
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task AddStudentFile(StudentAssignmentFile studentAssignmentFile)
        {
            await dbContext.StudentAssignmentFiles.AddAsync(studentAssignmentFile);
            await dbContext.SaveChangesAsync();
        }

        public async Task<StudentAssignmentFile> SendFile(Guid studentAssignmentId, Guid assignmentFileId, string Uri)
        {
            var studentAssignmentFile = await dbContext.StudentAssignmentFiles.FirstOrDefaultAsync(x => x.StudentAssignmentId == studentAssignmentId && x.AssignmentFileId == assignmentFileId);
            studentAssignmentFile.IsSended = true;
            studentAssignmentFile.Uri = Uri;

            await dbContext.SaveChangesAsync();
            return studentAssignmentFile;
        }

        public async Task<List<StudentAssignmentFile>> GetAllStudentAssignments()
        {
            var studentAssignmentFiles = await dbContext.StudentAssignmentFiles
                .Include(x => x.StudentAssignnment).ThenInclude(x => x.StudentCourse).ThenInclude(x => x.Student)
                .Include(x => x.StudentAssignnment).ThenInclude(x => x.CourseAssignment)
                .Include(x => x.AssignmentFile)
                .ToListAsync();
            return studentAssignmentFiles;
        }

        public async Task<StudentAssignmentFile> DeleteFile(Guid studentAssignmentFileId)
        {
            var studentAssignmentFile = await dbContext.StudentAssignmentFiles.FirstOrDefaultAsync(x => x.Id == studentAssignmentFileId);
            studentAssignmentFile.IsSended = false;
            studentAssignmentFile.Uri = null;

            await dbContext.SaveChangesAsync();
            return studentAssignmentFile;
        }
    }
}
