using Mentoroom.DOMAIN.Entities.Tags;
using Mentoroom.INFRASTRACTURE.Persistence;

namespace Mentoroom.INFRASTRACTURE.Seeders.Tags
{
    public class MajorsSeeder(AppDbContext dbContext)
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Majors.Any())
                {
                    var facultyOfMechanicalEngineering = dbContext.Departments.First(d => d.ShortName == "RM");
                    var facultyOfCivilEngineering = dbContext.Departments.First(d => d.ShortName == "RB");
                    var facultyOfElectricalEngineering = dbContext.Departments.First(d => d.ShortName == "RE");
                    var facultyOfAutomaticControlElectronicsAndComputerScience = dbContext.Departments.First(d => d.ShortName == "RAU");
                    var facultyOfChemicalEngineeringAndProcess = dbContext.Departments.First(d => d.ShortName == "RCH");
                    var facultyOfMathematics = dbContext.Departments.First(d => d.ShortName == "RMS");
                    var facultyOfArchitecture = dbContext.Departments.First(d => d.ShortName == "RAR");
                    var facultyOfPhysics = dbContext.Departments.First(d => d.ShortName == "RIF");
                    var facultyOfTransport = dbContext.Departments.First(d => d.ShortName == "RT");

                    var mechanicalEngineering = new Major() { Name = "Inżynieria Mechaniczna", DepartmentId = facultyOfMechanicalEngineering.Id };
                    var civilEngineering = new Major() { Name = "Budownictwo", DepartmentId = facultyOfCivilEngineering.Id };
                    var electricalEngineering = new Major() { Name = "Elektrotechnika", DepartmentId = facultyOfElectricalEngineering.Id };
                    var automaticControlElectronicsAndComputerScience = new Major() { Name = "Automatyka i Informatyka", DepartmentId = facultyOfAutomaticControlElectronicsAndComputerScience.Id };
                    var chemicalEngineeringAndProcess = new Major() { Name = "Inżynieria Chemiczna i Procesowa", DepartmentId = facultyOfChemicalEngineeringAndProcess.Id };
                    var mathematics = new Major() { Name = "Matematyka", DepartmentId = facultyOfMathematics.Id };
                    var informatics = new Major() { Name = "Informatyka", DepartmentId = facultyOfMathematics.Id };
                    var architecture = new Major() { Name = "Architektura", DepartmentId = facultyOfArchitecture.Id };
                    var interiorArchitecture = new Major() { Name = "Architektura Wnętrz", DepartmentId = facultyOfArchitecture.Id };
                    var mechanicalEngineeringConstructionOfMachines = new Major() { Name = "Mechanika i Budowa Maszyn", DepartmentId = facultyOfMechanicalEngineering.Id };
                    var industrialMechatronics = new Major() { Name = "Mechatronika Przemysłowa", DepartmentId = facultyOfMechanicalEngineering.Id };
                    var roadEngineering = new Major() { Name = "Budownictwo Drogowe", DepartmentId = facultyOfCivilEngineering.Id };
                    var powerEngineering = new Major() { Name = "Energetyka", DepartmentId = facultyOfElectricalEngineering.Id };
                    var roboticsAndAutomation = new Major() { Name = "Automatyka i Robotyka", DepartmentId = facultyOfAutomaticControlElectronicsAndComputerScience.Id };
                    var biotechnology = new Major() { Name = "Biotechnologia", DepartmentId = facultyOfAutomaticControlElectronicsAndComputerScience.Id };
                    var telecommunicationsEngineering = new Major() { Name = "Teleinformatyka", DepartmentId = facultyOfAutomaticControlElectronicsAndComputerScience.Id };
                    var technicalPhysics = new Major() { Name = "Fizyka Techniczna", DepartmentId = facultyOfPhysics.Id };
                    var geoinformatics = new Major() { Name = "Geoinformatyka", DepartmentId = facultyOfPhysics.Id };
                    var transportEngineering = new Major() { Name = "Transport", DepartmentId = facultyOfTransport.Id };
                    var railwayTransportEngineering = new Major() { Name = "Transport Kolejowy", DepartmentId = facultyOfTransport.Id };
                    var aerospaceEngineering = new Major() { Name = "Inżynieria Lotnicza i Kosmiczna", DepartmentId = facultyOfTransport.Id };

                    dbContext.Majors.Add(mathematics);
                    dbContext.Majors.Add(informatics);
                    dbContext.Majors.Add(architecture);
                    dbContext.Majors.Add(interiorArchitecture);
                    dbContext.Majors.Add(mechanicalEngineeringConstructionOfMachines);
                    dbContext.Majors.Add(industrialMechatronics);
                    dbContext.Majors.Add(roadEngineering);
                    dbContext.Majors.Add(powerEngineering);
                    dbContext.Majors.Add(roboticsAndAutomation);
                    dbContext.Majors.Add(biotechnology);
                    dbContext.Majors.Add(telecommunicationsEngineering);
                    dbContext.Majors.Add(technicalPhysics);
                    dbContext.Majors.Add(geoinformatics);
                    dbContext.Majors.Add(transportEngineering);
                    dbContext.Majors.Add(railwayTransportEngineering);
                    dbContext.Majors.Add(aerospaceEngineering);
                    dbContext.Majors.Add(mechanicalEngineering);
                    dbContext.Majors.Add(civilEngineering);
                    dbContext.Majors.Add(electricalEngineering);
                    dbContext.Majors.Add(automaticControlElectronicsAndComputerScience);
                    dbContext.Majors.Add(chemicalEngineeringAndProcess);

                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
