using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Entities.Tags;
using Mentoroom.DOMAIN.Models;
using Mentoroom.INFRASTRACTURE.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Mentoroom.INFRASTRACTURE.Seeders.Shared
{
    public class CourseSeeder(AppDbContext dbContext, UserManager<AppUser> userManager)
    {
        private readonly AppDbContext dbContext = dbContext;
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Courses.Any())
                {
                    var lecturers = await userManager.GetUsersInRoleAsync(UserRoles.Lecturer);
                    var lecturer = (await userManager.GetUsersInRoleAsync(UserRoles.Lecturer)).FirstOrDefault(u => u.Email == "lecturer@lecturer.com");

                    var facultyOfMechanicalEngineering = dbContext.Departments.First(d => d.ShortName == "RM");
                    var facultyOfCivilEngineering = dbContext.Departments.First(d => d.ShortName == "RB");
                    var facultyOfElectricalEngineering = dbContext.Departments.First(d => d.ShortName == "RE");
                    var facultyOfMathematics = dbContext.Departments.First(d => d.ShortName == "RMS");
                    var facultyOfArchitecture = dbContext.Departments.First(d => d.ShortName == "RAR");
                    var facultyOfPhysics = dbContext.Departments.First(d => d.ShortName == "RIF");
                    var facultyOfTransport = dbContext.Departments.First(d => d.ShortName == "RT");

                    var mechanicalEngineering = dbContext.Majors.First(d => d.Name == "Inżynieria Mechaniczna");
                    var civilEngineering = dbContext.Majors.First(d => d.Name == "Budownictwo");
                    var electricalEngineering = dbContext.Majors.First(d => d.Name == "Elektrotechnika");
                    var informatics = dbContext.Majors.First(d => d.Name == "Informatyka");
                    var architecture = dbContext.Majors.First(d => d.Name == "Architektura");
                    var geoinformatics = dbContext.Majors.First(d => d.Name == "Geoinformatyka");
                    var transportEngineering = dbContext.Majors.First(d => d.Name == "Transport");
                    var mathematics = dbContext.Majors.First(d => d.Name == "Matematyka");
                    var roadEngineering = dbContext.Majors.First(d => d.Name == "Budownictwo Drogowe");


                    var bachelorDegree = dbContext.Degrees.First(d => d.Name == "Bachelor's");
                    var mastersDegree = dbContext.Degrees.First(d => d.Name == "Master's");
                    var phdDegree = dbContext.Degrees.First(d => d.Name == "Doctorate");

                    var year1 = dbContext.Years.First(y => y.Name == "1");
                    var year2 = dbContext.Years.First(y => y.Name == "2");
                    var year1Degree2 = dbContext.Years.First(y => y.Name == "1" && y.DegreeId == mastersDegree.Id);
                    var year1Degree3 = dbContext.Years.First(y => y.Name == "1" && y.DegreeId == phdDegree.Id);

                    var semester1 = dbContext.Semesters.First(s => s.Name == "1" && s.YearId == year1.Id);
                    var semester2 = dbContext.Semesters.First(s => s.Name == "2" && s.YearId == year1.Id);
                    var semester3 = dbContext.Semesters.First(s => s.Name == "1" && s.YearId == year2.Id);
                    var semester4 = dbContext.Semesters.First(s => s.Name == "2" && s.YearId == year2.Id);
                    var semester5 = dbContext.Semesters.First(s => s.Name == "1" && s.YearId == year1Degree2.Id);
                    var semester6 = dbContext.Semesters.First(s => s.Name == "2" && s.YearId == year1Degree2.Id);
                    var semester7 = dbContext.Semesters.First(s => s.Name == "1" && s.YearId == year1Degree3.Id);

                    var course1 = new Course()
                    {
                        Name = "Programowanie",
                        Description = "Programowanie w c++",
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        Tags = new CourseTags()
                        {
                            Department = facultyOfMechanicalEngineering.Name,
                            ShortDepartment = facultyOfMechanicalEngineering.ShortName,
                            Major = mechanicalEngineering.Name,
                            Degree = bachelorDegree.Name,
                            Year = year1.Name,
                            Semester = semester1.Name
                        },
                        Author = lecturer,
                    };

                    var course2 = new Course()
                    {
                        Name = "Inżynieria budowlana",
                        Description = "Podstawy inżynierii budowlanej",
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        Tags = new CourseTags()
                        {
                            Department = facultyOfCivilEngineering.Name,
                            ShortDepartment = facultyOfCivilEngineering.ShortName,
                            Major = civilEngineering.Name,
                            Degree = bachelorDegree.Name,
                            Year = year1.Name,
                            Semester = semester2.Name
                        },
                        Author = lecturers[2]
                    };

                    var course3 = new Course()
                    {
                        Name = "Teoria obwodów",
                        Description = "Podstawy teorii obwodów elektrycznych",
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        Tags = new CourseTags()
                        {
                            Department = facultyOfElectricalEngineering.Name,
                            ShortDepartment = facultyOfElectricalEngineering.ShortName,
                            Major = electricalEngineering.Name,
                            Degree = bachelorDegree.Name,
                            Year = year2.Name,
                            Semester = semester3.Name
                        },
                        Author = lecturers[0]
                    };

                    var course4 = new Course()
                    {
                        Name = "Pomiary elektryczne w przemyśle",
                        Description = "Niezawodność i wydajność systemów elektrycznych",
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        Tags = new CourseTags()
                        {
                            Department = facultyOfElectricalEngineering.Name,
                            ShortDepartment = facultyOfElectricalEngineering.ShortName,
                            Major = electricalEngineering.Name,
                            Degree = mastersDegree.Name,
                            Year = year1Degree2.Name,
                            Semester = semester5.Name
                        },
                        Author = lecturers[3]
                    };

                    var course5 = new Course()
                    {
                        Name = "Budownictwo Drogowe",
                        Description = "Cienkie czy szerokie?",
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        Tags = new CourseTags()
                        {
                            Department = facultyOfElectricalEngineering.Name,
                            ShortDepartment = facultyOfElectricalEngineering.ShortName,
                            Major = electricalEngineering.Name,
                            Degree = mastersDegree.Name,
                            Year = year1Degree2.Name,
                            Semester = semester6.Name
                        },
                        Author = lecturers[0]
                    };

                    var course6 = new Course()
                    {
                        Name = "Automatyka w elektrotechnice",
                        Description = "Automatyzacja procesów związanych z energią elektryczną",
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        Tags = new CourseTags()
                        {
                            Department = facultyOfElectricalEngineering.Name,
                            ShortDepartment = facultyOfElectricalEngineering.ShortName,
                            Major = electricalEngineering.Name,
                            Degree = phdDegree.Name,
                            Year = year1Degree3.Name,
                            Semester = semester7.Name
                        },
                        Author = lecturers[1]
                    };
                    var course7 = new Course()
                    {
                        Name = "Algorytmy i struktury danych",
                        Description = "Podstawy algorytmów i struktur danych",
                        CreatedDate = DateTime.Now,
                        IsActive = false,
                        Tags = new CourseTags()
                        {
                            Department = facultyOfMathematics.Name,
                            ShortDepartment = facultyOfMathematics.ShortName,
                            Major = informatics.Name,
                            Degree = mastersDegree.Name,
                            Year = year1Degree2.Name,
                            Semester = semester6.Name
                        },
                        Author = lecturers[2]
                    };

                    var course8 = new Course()
                    {
                        Name = "Analiza matematyczna",
                        Description = "Podstawy analizy matematycznej",
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        Tags = new CourseTags()
                        {
                            Department = facultyOfMathematics.Name,
                            ShortDepartment = facultyOfMathematics.ShortName,
                            Major = mathematics.Name,
                            Degree = bachelorDegree.Name,
                            Year = year2.Name,
                            Semester = semester3.Name
                        },
                        Author = lecturers[8]
                    };

                    var course9 = new Course()
                    {
                        Name = "Konstrukcje betonowe",
                        Description = "Projektowanie i budowa konstrukcji betonowych",
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        Tags = new CourseTags()
                        {
                            Department = facultyOfCivilEngineering.Name,
                            ShortDepartment = facultyOfCivilEngineering.ShortName,
                            Major = civilEngineering.Name,
                            Degree = bachelorDegree.Name,
                            Year = year1.Name,
                            Semester = semester2.Name
                        },
                        Author = lecturers[6]
                    };

                    var course10 = new Course()
                    {
                        Name = "Podstawy elektroniki",
                        Description = "Wprowadzenie do podstaw elektroniki",
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        Tags = new CourseTags()
                        {
                            Department = facultyOfElectricalEngineering.Name,
                            ShortDepartment = facultyOfElectricalEngineering.ShortName,
                            Major = electricalEngineering.Name,
                            Degree = bachelorDegree.Name,
                            Year = year2.Name,
                            Semester = semester4.Name
                        },
                        Author = lecturers[5]
                    };
                    var course11 = new Course()
                    {
                        Name = "Projektowanie architektoniczne",
                        Description = "Podstawy projektowania budynków i przestrzeni",
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        Tags = new CourseTags()
                        {
                            Department = facultyOfArchitecture.Name,
                            ShortDepartment = facultyOfArchitecture.ShortName,
                            Major = architecture.Name,
                            Degree = mastersDegree.Name,
                            Year = year1Degree2.Name,
                            Semester = semester5.Name
                        },
                        Author = lecturers[4]
                    };

                    var course12 = new Course()
                    {
                        Name = "Analiza danych geoprzestrzennych",
                        Description = "Metody analizy danych geoprzestrzennych",
                        CreatedDate = DateTime.Now,
                        IsActive = false,
                        Tags = new CourseTags()
                        {
                            Department = facultyOfPhysics.Name,
                            ShortDepartment = facultyOfPhysics.ShortName,
                            Major = geoinformatics.Name,
                            Degree = mastersDegree.Name,
                            Year = year1Degree2.Name,
                            Semester = semester6.Name
                        },
                        Author = lecturers[2]
                    };

                    var course13 = new Course()
                    {
                        Name = "Logistyka transportu",
                        Description = "Podstawy logistyki w transporcie",
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        Tags = new CourseTags()
                        {
                            Department = facultyOfTransport.Name,
                            ShortDepartment = facultyOfTransport.ShortName,
                            Major = transportEngineering.Name,
                            Degree = bachelorDegree.Name,
                            Year = year2.Name,
                            Semester = semester3.Name
                        },
                        Author = lecturers[3]
                    };

                    var course14 = new Course()
                    {
                        Name = "Algebra liniowa",
                        Description = "Podstawy algebry liniowej",
                        CreatedDate = DateTime.Now,
                        IsActive = false,
                        Tags = new CourseTags()
                        {
                            Department = facultyOfMathematics.Name,
                            ShortDepartment = facultyOfMathematics.ShortName,
                            Major = mathematics.Name,
                            Degree = bachelorDegree.Name,
                            Year = year1.Name,
                            Semester = semester2.Name
                        },
                        Author = lecturers[0]
                    };

                    var course15 = new Course()
                    {
                        Name = "Zarządzanie projektami drogowymi",
                        Description = "Metody zarządzania projektami drogowymi",
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        Tags = new CourseTags()
                        {
                            Department = facultyOfCivilEngineering.Name,
                            ShortDepartment = facultyOfCivilEngineering.ShortName,
                            Major = roadEngineering.Name,
                            Degree = bachelorDegree.Name,
                            Year = year2.Name,
                            Semester = semester4.Name
                        },
                        Author = lecturers[1]
                    };

                    var course16 = new Course()
                    {
                        Name = "Projektowanie urbanistyczne",
                        Description = "Podstawy projektowania przestrzeni miejskiej",
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        Tags = new CourseTags()
                        {
                            Department = facultyOfArchitecture.Name,
                            ShortDepartment = facultyOfArchitecture.ShortName,
                            Major = architecture.Name,
                            Degree = mastersDegree.Name,
                            Year = year1Degree2.Name,
                            Semester = semester6.Name
                        },
                        Author = lecturers[2]
                    };

                    dbContext.Courses.Add(course1);
                    dbContext.Courses.Add(course2);
                    dbContext.Courses.Add(course3);
                    dbContext.Courses.Add(course4);
                    dbContext.Courses.Add(course5);
                    dbContext.Courses.Add(course6);
                    dbContext.Courses.Add(course7);
                    dbContext.Courses.Add(course8);
                    dbContext.Courses.Add(course9);
                    dbContext.Courses.Add(course10);
                    dbContext.Courses.Add(course11);
                    dbContext.Courses.Add(course12);
                    dbContext.Courses.Add(course13);
                    dbContext.Courses.Add(course14);
                    dbContext.Courses.Add(course15);
                    dbContext.Courses.Add(course16);

                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
