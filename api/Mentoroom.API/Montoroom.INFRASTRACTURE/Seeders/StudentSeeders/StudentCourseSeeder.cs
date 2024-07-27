using Mentoroom.DOMAIN.Entities.StudentModels;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.INFRASTRACTURE.Seeders.StudentSeeders
{
    public class StudentCourseSeeder(AppDbContext dbContext)
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.StudentCourses.Any())
                {

                    var students = await dbContext.Students.ToListAsync();

                    var course1 = dbContext.Courses.First(d => d.Name == "Programowanie");
                    var course2 = dbContext.Courses.First(d => d.Name == "Inżynieria budowlana");
                    var course3 = dbContext.Courses.First(d => d.Name == "Teoria obwodów");
                    var course4 = dbContext.Courses.First(d => d.Name == "Automatyka w elektrotechnice");

                    var studentCourse1 = new StudentCourse()
                    {
                        JoiningDate = DateTime.Now,
                        StudentId = students[0].UserId,
                        Student = students[0],
                        CourseId = course1.Id,
                        Course = course1,
                        IsConfirmed = true
                    };


                    var studentCourse2 = new StudentCourse()
                    {
                        JoiningDate = DateTime.Now,
                        StudentId = students[1].UserId,
                        Student = students[1],
                        CourseId = course2.Id,
                        Course = course2,
                        IsConfirmed = true
                    };

                    var studentCourse3 = new StudentCourse()
                    {
                        JoiningDate = DateTime.Now,
                        StudentId = students[2].UserId,
                        Student = students[2],
                        CourseId = course3.Id,
                        Course = course3,
                        IsConfirmed = true
                    };

                    var studentCourse4 = new StudentCourse()
                    {
                        JoiningDate = DateTime.Now,
                        StudentId = students[3].UserId,
                        Student = students[3],
                        CourseId = course4.Id,
                        Course = course4
                    };

                    var studentCourse5 = new StudentCourse()
                    {
                        JoiningDate = DateTime.Now,
                        StudentId = students[1].UserId,
                        Student = students[1],
                        CourseId = course1.Id,
                        Course = course1,
                        IsConfirmed = true
                    };


                    var studentCourse6 = new StudentCourse()
                    {
                        JoiningDate = DateTime.Now,
                        StudentId = students[2].UserId,
                        Student = students[2],
                        CourseId = course2.Id,
                        Course = course2,
                        IsConfirmed = true
                    };

                    var studentCourse7 = new StudentCourse()
                    {
                        JoiningDate = DateTime.Now,
                        StudentId = students[3].UserId,
                        Student = students[3],
                        CourseId = course3.Id,
                        Course = course3,
                        IsConfirmed = true
                    };

                    var studentCourse8 = new StudentCourse()
                    {
                        JoiningDate = DateTime.Now,
                        StudentId = students[4].UserId,
                        Student = students[4],
                        CourseId = course4.Id,
                        Course = course4
                    };

                    dbContext.StudentCourses.AddRange(studentCourse1, studentCourse2, studentCourse3, studentCourse4, studentCourse5, studentCourse6, studentCourse7, studentCourse8);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
