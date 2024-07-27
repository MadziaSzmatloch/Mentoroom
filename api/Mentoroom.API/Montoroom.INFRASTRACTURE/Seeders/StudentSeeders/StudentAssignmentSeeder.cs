using Mentoroom.DOMAIN.Entities.StudentModels;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.INFRASTRACTURE.Seeders.StudentSeeders
{
    public class StudentAssignmentSeeder(AppDbContext dbContext)
    {
        private readonly AppDbContext dbContext = dbContext;
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.StudentAssignments.Any())
                {
                    var studentcourses = await dbContext.StudentCourses
                        .Include(sc => sc.Course).ThenInclude(c => c.Assignments)
                        .ToListAsync();

                    var studentAssignments = new List<StudentAssignment>();
                    foreach (var studentcourse in studentcourses)
                    {
                        if (studentcourse.IsConfirmed == true)
                        {
                            foreach (var assignment in studentcourse.Course.Assignments)
                            {
                                studentAssignments.Add(new StudentAssignment()
                                {
                                    IsCompleted = false,
                                    StudentCourseId = studentcourse.Id,
                                    CourseAssignmentId = assignment.Id
                                });
                            }
                        }
                    }

                    foreach (var studentAssignment in studentAssignments)
                    {
                        dbContext.StudentAssignments.Add(studentAssignment);
                    }

                    await dbContext.SaveChangesAsync();

                }
            }
        }
    }
}
