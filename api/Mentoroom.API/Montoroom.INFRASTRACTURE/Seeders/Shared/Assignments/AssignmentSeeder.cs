using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.INFRASTRACTURE.Persistence;

namespace Mentoroom.INFRASTRACTURE.Seeders.Shared.Assignments
{
    public class AssignmentSeeder(AppDbContext dbContext)
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Assignments.Any())
                {
                    var course1 = dbContext.Courses.First(d => d.Name == "Programowanie");
                    var course2 = dbContext.Courses.First(d => d.Name == "Inżynieria budowlana");
                    var course3 = dbContext.Courses.First(d => d.Name == "Teoria obwodów");
                    var course4 = dbContext.Courses.First(d => d.Name == "Automatyka w elektrotechnice");

                    var assignment1 = new CourseAssignment()
                    {
                        Name = "Assignment 1",
                        Description = "First assignment",
                        CourseId = course1.Id
                    };

                    var assignment2 = new CourseAssignment()
                    {
                        Name = "Assignment 2",
                        Description = "Second assignment",
                        CourseId = course1.Id
                    };

                    var assignment3 = new CourseAssignment()
                    {
                        Name = "Assignment 3",
                        Description = "Third assignment",
                        CourseId = course2.Id
                    };

                    var assignment4 = new CourseAssignment()
                    {
                        Name = "Napisz kalkulator",
                        Description = "Fourth assignment",
                        CourseId = course1.Id
                    };

                    var assignment5 = new CourseAssignment()
                    {
                        Name = "Assignment 5",
                        Description = "Fifth assignment",
                        CourseId = course3.Id
                    };

                    var assignment6 = new CourseAssignment()
                    {
                        Name = "Assignment 6",
                        Description = "Sixth assignment",
                        CourseId = course3.Id
                    };

                    var assignment7 = new CourseAssignment()
                    {
                        Name = "Assignment 7",
                        Description = "Seventh assignment",
                        CourseId = course4.Id
                    };

                    var assignment8 = new CourseAssignment()
                    {
                        Name = "Assignment 8",
                        Description = "Eighth assignment",
                        CourseId = course4.Id
                    };

                    var assignment9 = new CourseAssignment()
                    {
                        Name = "Assignment 9",
                        Description = "Nine assignment",
                        CourseId = course1.Id
                    };

                    var assignment10 = new CourseAssignment()
                    {
                        Name = "Assignment 10",
                        Description = "Ten assignment",
                        CourseId = course1.Id
                    };

                    var assignment11 = new CourseAssignment()
                    {
                        Name = "Assignment 11",
                        Description = "Eleven assignment",
                        CourseId = course1.Id
                    };

                    var assignment12 = new CourseAssignment()
                    {
                        Name = "Assignment 12",
                        Description = "Twelve assignment",
                        CourseId = course1.Id
                    };

                    var assignment13 = new CourseAssignment()
                    {
                        Name = "Assignment 13",
                        Description = "Eighth assignment",
                        CourseId = course1.Id
                    };

                    var assignment14 = new CourseAssignment()
                    {
                        Name = "Assignment 14",
                        Description = "Fourteen assignment",
                        CourseId = course1.Id
                    };

                    var assignment15 = new CourseAssignment()
                    {
                        Name = "Assignment 15",
                        Description = "Fifteenth assignment",
                        CourseId = course1.Id
                    };

                    var assignment16 = new CourseAssignment()
                    {
                        Name = "Assignment 16",
                        Description = "Sixteenth assignment",
                        CourseId = course1.Id
                    };

                    var assignment17 = new CourseAssignment()
                    {
                        Name = "Assignment 17",
                        Description = "Seventeenth assignment",
                        CourseId = course1.Id
                    };

                    var assignment18 = new CourseAssignment()
                    {
                        Name = "Assignment 18",
                        Description = "Eighteenth assignment",
                        CourseId = course1.Id
                    };

                    var assignment19 = new CourseAssignment()
                    {
                        Name = "Assignment 19",
                        Description = "Nineteenth assignment",
                        CourseId = course1.Id
                    };

                    var assignment20 = new CourseAssignment()
                    {
                        Name = "Assignment 20",
                        Description = "Twentieth assignment",
                        CourseId = course1.Id
                    };

                    var assignment21 = new CourseAssignment()
                    {
                        Name = "Assignment 21",
                        Description = "Twenty-first assignment",
                        CourseId = course1.Id
                    };

                    var assignment22 = new CourseAssignment()
                    {
                        Name = "Assignment 22",
                        Description = "Twenty-second assignment",
                        CourseId = course1.Id
                    };

                    var assignment23 = new CourseAssignment()
                    {
                        Name = "Assignment 23",
                        Description = "Twenty-third assignment",
                        CourseId = course1.Id
                    };

                    var assignment24 = new CourseAssignment()
                    {
                        Name = "Assignment 24",
                        Description = "Twenty-fourth assignment",
                        CourseId = course1.Id
                    };


                    dbContext.Assignments.Add(assignment1);
                    dbContext.Assignments.Add(assignment2);
                    dbContext.Assignments.Add(assignment3);
                    dbContext.Assignments.Add(assignment4);
                    dbContext.Assignments.Add(assignment5);
                    dbContext.Assignments.Add(assignment6);
                    dbContext.Assignments.Add(assignment7);
                    dbContext.Assignments.Add(assignment8);
                    dbContext.Assignments.Add(assignment9);
                    dbContext.Assignments.Add(assignment10);
                    dbContext.Assignments.Add(assignment11);
                    dbContext.Assignments.Add(assignment12);
                    dbContext.Assignments.Add(assignment13);
                    dbContext.Assignments.Add(assignment14);
                    dbContext.Assignments.Add(assignment15);
                    dbContext.Assignments.Add(assignment16);
                    dbContext.Assignments.Add(assignment17);
                    dbContext.Assignments.Add(assignment18);
                    dbContext.Assignments.Add(assignment19);
                    dbContext.Assignments.Add(assignment20);
                    dbContext.Assignments.Add(assignment21);
                    dbContext.Assignments.Add(assignment22);
                    dbContext.Assignments.Add(assignment23);
                    dbContext.Assignments.Add(assignment24);

                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
