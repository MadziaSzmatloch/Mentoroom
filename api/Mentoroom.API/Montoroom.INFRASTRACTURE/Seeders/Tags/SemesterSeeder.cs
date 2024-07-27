using Mentoroom.DOMAIN.Entities.Tags;
using Mentoroom.INFRASTRACTURE.Persistence;

namespace Mentoroom.INFRASTRACTURE.Seeders.Tags
{
    public class SemesterSeeder(AppDbContext dbContext)
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Semesters.Any())
                {
                    var bachelorsDegree = dbContext.Degrees.First(d => d.Name == "Bachelor's");
                    var year1Degree1 = dbContext.Years.First(y => y.Name == "1" && y.DegreeId == bachelorsDegree.Id);
                    var year2Degree1 = dbContext.Years.First(y => y.Name == "2" && y.DegreeId == bachelorsDegree.Id);
                    var year3Degree1 = dbContext.Years.First(y => y.Name == "3" && y.DegreeId == bachelorsDegree.Id);

                    var mastersDegree = dbContext.Degrees.First(d => d.Name == "Master's");
                    var year1Degree2 = dbContext.Years.First(y => y.Name == "1" && y.DegreeId == mastersDegree.Id);
                    var year2Degree2 = dbContext.Years.First(y => y.Name == "2" && y.DegreeId == mastersDegree.Id);
                    var year3Degree2 = dbContext.Years.First(y => y.Name == "3" && y.DegreeId == mastersDegree.Id);

                    var phdDegree = dbContext.Degrees.First(d => d.Name == "Doctorate");
                    var year1Degree3 = dbContext.Years.First(y => y.Name == "1" && y.DegreeId == phdDegree.Id);
                    var year2Degree3 = dbContext.Years.First(y => y.Name == "2" && y.DegreeId == phdDegree.Id);
                    var year3Degree3 = dbContext.Years.First(y => y.Name == "3" && y.DegreeId == phdDegree.Id);

                    var semster1 = new Semester() { Name = "1", YearId = year1Degree1.Id };
                    var semster2 = new Semester() { Name = "2", YearId = year1Degree1.Id };

                    var semster3 = new Semester() { Name = "1", YearId = year2Degree1.Id };
                    var semster4 = new Semester() { Name = "2", YearId = year2Degree1.Id };

                    var semster5 = new Semester() { Name = "1", YearId = year3Degree1.Id };
                    var semster6 = new Semester() { Name = "2", YearId = year3Degree1.Id };

                    var semster7 = new Semester() { Name = "1", YearId = year1Degree2.Id };
                    var semster8 = new Semester() { Name = "2", YearId = year1Degree2.Id };

                    var semster9 = new Semester() { Name = "1", YearId = year2Degree2.Id };
                    var semster10 = new Semester() { Name = "2", YearId = year2Degree2.Id };

                    var semster11 = new Semester() { Name = "1", YearId = year3Degree2.Id };
                    var semster12 = new Semester() { Name = "2", YearId = year3Degree2.Id };

                    var semster13 = new Semester() { Name = "1", YearId = year1Degree3.Id };
                    var semster14 = new Semester() { Name = "2", YearId = year1Degree3.Id };

                    var semster15 = new Semester() { Name = "1", YearId = year2Degree3.Id };
                    var semster16 = new Semester() { Name = "2", YearId = year2Degree3.Id };

                    var semster17 = new Semester() { Name = "1", YearId = year3Degree3.Id };
                    var semster18 = new Semester() { Name = "2", YearId = year3Degree3.Id };

                    dbContext.Semesters.Add(semster1);
                    dbContext.Semesters.Add(semster2);
                    dbContext.Semesters.Add(semster3);
                    dbContext.Semesters.Add(semster4);
                    dbContext.Semesters.Add(semster5);
                    dbContext.Semesters.Add(semster6);
                    dbContext.Semesters.Add(semster7); 
                    dbContext.Semesters.Add(semster8);
                    dbContext.Semesters.Add(semster9);
                    dbContext.Semesters.Add(semster10);
                    dbContext.Semesters.Add(semster11);
                    dbContext.Semesters.Add(semster12);
                    dbContext.Semesters.Add(semster13);
                    dbContext.Semesters.Add(semster14);
                    dbContext.Semesters.Add(semster15);
                    dbContext.Semesters.Add(semster16);
                    dbContext.Semesters.Add(semster17);
                    dbContext.Semesters.Add(semster18);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
