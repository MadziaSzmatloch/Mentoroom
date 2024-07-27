using Mentoroom.DOMAIN.Entities.Tags;
using Mentoroom.INFRASTRACTURE.Persistence;

namespace Mentoroom.INFRASTRACTURE.Seeders.Tags
{
    public class DepartmentSeeder(AppDbContext dbContext)
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Departments.Any())
                {
                    var facultyOfMechanicalEngineering = new Department() { Name = "Wydział Mechaniczny", ShortName = "RM" };
                    var facultyOfCivilEngineering = new Department() { Name = "Wydział Budownictwa", ShortName = "RB" };
                    var facultyOfElectricalEngineering = new Department() { Name = "Wydział Elektryczny", ShortName = "RE" };
                    var facultyOfAutomaticControlElectronicsAndComputerScience = new Department() { Name = "Wydział Automatyki, Elektroniki i Informatyki", ShortName = "RAU" };
                    var facultyOfChemicalEngineeringAndProcess = new Department() { Name = "Wydział Chemiczny", ShortName = "RCH" };
                    var facultyOfMathematics = new Department() { Name = "Wydział Matematyki Stosowanej", ShortName = "RMS" };
                    var facultyOfArchitecture = new Department() { Name = "Wydział Architektury", ShortName = "RAR" };
                    var facultyOfPhysics = new Department() { Name = "Wydział Fizyki", ShortName = "RIF" };
                    var facultyOfTransport = new Department() { Name = "Wydział Transportu", ShortName = "RT" };

                    dbContext.Departments.Add(facultyOfMechanicalEngineering);
                    dbContext.Departments.Add(facultyOfCivilEngineering);
                    dbContext.Departments.Add(facultyOfElectricalEngineering);
                    dbContext.Departments.Add(facultyOfAutomaticControlElectronicsAndComputerScience);
                    dbContext.Departments.Add(facultyOfChemicalEngineeringAndProcess);
                    dbContext.Departments.Add(facultyOfMathematics);
                    dbContext.Departments.Add(facultyOfArchitecture);
                    dbContext.Departments.Add(facultyOfPhysics);
                    dbContext.Departments.Add(facultyOfTransport);

                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
