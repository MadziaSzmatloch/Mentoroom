using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Entities.StudentModels;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;
using Mentoroom.DOMAIN.Models;
using Microsoft.AspNetCore.Identity;

namespace Mentoroom.INFRASTRACTURE.Seeders
{
    public class AuthSeeder(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, IStudentRepository studentRepository)
    {
        private readonly RoleManager<IdentityRole> roleManager = roleManager;
        private readonly UserManager<AppUser> userManager = userManager;
        private readonly IStudentRepository studentRepository = studentRepository;

        public async Task Seed()
        {
            await SeedRoles();
            await SeedUsers();
        }

        private async Task SeedRoles()
        {
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            if (!await roleManager.RoleExistsAsync(UserRoles.Lecturer))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Lecturer));
            }

            if (!await roleManager.RoleExistsAsync(UserRoles.Student))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Student));
            }
        }

        private async Task SeedUsers()
        {
            if (!userManager.Users.Any())
            {
                await CreateUserWithRole("Admin", "Admin", "admin@admin.com", "Admin2002!", UserRoles.Admin);
                await CreateUserWithRole("Lecturer", "Lecturer", "lecturer@lecturer.com", "Lecturer2002!", UserRoles.Lecturer);
                await CreateUserWithRole("Student", "Student", "student@student.com", "Student2002!", UserRoles.Student);

                //seed guest without selected role
                await CreateUserWithRole("Guest", "Guest", "guest@guest.com", "Guest2002!", null);

                await SeedPlaceholderUsers();
            }
        }

        private async Task CreateUserWithRole(string firstName, string lastName, string email, string password, string? role)
        {
            var user = new AppUser()
            {
                UserName = email,
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded && role != null)
            {
                await userManager.AddToRoleAsync(user, role);
            }

            if (role == UserRoles.Student)
            {
                var addedUser = await userManager.FindByEmailAsync(email);

                if (addedUser != null)
                {
                    await studentRepository.AddStudent(new Student()
                    {
                        UserId = addedUser.Id,
                        IndexNumber = addedUser.FirstName
                    });
                }
            }
        }

        public async Task SeedPlaceholderUsers()
        {
            await CreateUserWithRole("John", "Doe", "john@example.com", "JohnDoe1!", "Student");
            await CreateUserWithRole("Jane", "Smith", "jane@example.com", "JaneSmith2!", "Lecturer");
            await CreateUserWithRole("Michael", "Johnson", "michael@example.com", "MichaelJohnson3!", "Student");
            await CreateUserWithRole("Emily", "Brown", "emily@example.com", "EmilyBrown4!", "Student");
            await CreateUserWithRole("William", "Wilson", "william@example.com", "WilliamWilson5!", "Lecturer");
            await CreateUserWithRole("Emma", "Jones", "emma@example.com", "EmmaJones6!", "Student");
            await CreateUserWithRole("Daniel", "Taylor", "daniel@example.com", "DanielTaylor7!", "Student");
            await CreateUserWithRole("Olivia", "Anderson", "olivia@example.com", "OliviaAnderson8!", "Lecturer");
            await CreateUserWithRole("David", "Thomas", "david@example.com", "DavidThomas9!", "Student");
            await CreateUserWithRole("Sophia", "Martinez", "sophia@example.com", "SophiaMartinez10!", "Student");
            await CreateUserWithRole("James", "Wilson", "james@example.com", "JamesWilson11!", "Lecturer");
            await CreateUserWithRole("Grace", "White", "grace@example.com", "GraceWhite12!", "Student");
            await CreateUserWithRole("Alexander", "Harris", "alexander@example.com", "AlexanderHarris13!", "Student");
            await CreateUserWithRole("Ava", "Martin", "ava@example.com", "AvaMartin14!", "Lecturer");
            await CreateUserWithRole("Ethan", "Thompson", "ethan@example.com", "EthanThompson15!", "Student");
            await CreateUserWithRole("Mia", "Garcia", "mia@example.com", "MiaGarcia16!", "Student");
            await CreateUserWithRole("Benjamin", "Lee", "benjamin@example.com", "BenjaminLee17!", "Lecturer");
            await CreateUserWithRole("Isabella", "Clark", "isabella@example.com", "IsabellaClark18!", "Student");
            await CreateUserWithRole("Samuel", "Walker", "samuel@example.com", "SamuelWalker19!", "Student");
            await CreateUserWithRole("Madison", "Hall", "madison@example.com", "MadisonHall20!", "Lecturer");
            await CreateUserWithRole("Gabriel", "Allen", "gabriel@example.com", "GabrielAllen21!", "Student");
            await CreateUserWithRole("Charlotte", "Young", "charlotte@example.com", "CharlotteYoung22!", "Student");
            await CreateUserWithRole("Logan", "Lewis", "logan@example.com", "LoganLewis23!", "Lecturer");
            await CreateUserWithRole("Sofia", "Lopez", "sofia@example.com", "SofiaLopez24!", "Student");
            await CreateUserWithRole("Christopher", "King", "christopher@example.com", "ChristopherKing25!", "Student");
            await CreateUserWithRole("Harper", "Adams", "harper@example.com", "HarperAdams26!", "Lecturer");
            await CreateUserWithRole("Zoe", "Baker", "zoe@example.com", "ZoeBaker27!", "Student");
            await CreateUserWithRole("Andrew", "Green", "andrew@example.com", "AndrewGreen28!", "Student");
            await CreateUserWithRole("Evelyn", "Evans", "evelyn@example.com", "EvelynEvans29!", "Lecturer");
            await CreateUserWithRole("Joshua", "Morris", "joshua@example.com", "JoshuaMorris30!", "Student");
            await CreateUserWithRole("Avery", "Hill", "avery@example.com", "AveryHill31!", "Student");
            await CreateUserWithRole("Victoria", "Parker", "victoria@example.com", "VictoriaParker32!", "Lecturer");
            await CreateUserWithRole("Ryan", "Wright", "ryan@example.com", "RyanWright33!", "Student");
            await CreateUserWithRole("Liam", "Turner", "liam@example.com", "LiamTurner34!", "Student");
            await CreateUserWithRole("Natalie", "Roberts", "natalie@example.com", "NatalieRoberts35!", "Lecturer");
            await CreateUserWithRole("Emma", "Phillips", "emma2@example.com", "EmmaPhillips36!", "Student");
            await CreateUserWithRole("Nicholas", "Scott", "nicholas@example.com", "NicholasScott37!", "Student");
            await CreateUserWithRole("Aria", "Turner", "aria@example.com", "AriaTurner38!", "Lecturer");
            await CreateUserWithRole("Jackson", "White", "jackson@example.com", "JacksonWhite39!", "Student");
            await CreateUserWithRole("Hannah", "Cook", "hannah@example.com", "HannahCook40!", "Student");
            await CreateUserWithRole("Lincoln", "Nelson", "lincoln@example.com", "LincolnNelson41!", "Lecturer");
            await CreateUserWithRole("Addison", "Hill", "addison@example.com", "AddisonHill42!", "Student");
            await CreateUserWithRole("Aubrey", "King", "aubrey@example.com", "AubreyKing43!", "Student");
            await CreateUserWithRole("Christopher", "Brown", "christopher2@example.com", "ChristopherBrown44!", "Lecturer");
            await CreateUserWithRole("Elizabeth", "Gonzalez", "elizabeth@example.com", "ElizabethGonzalez45!", "Student");
            await CreateUserWithRole("Sebastian", "Mitchell", "sebastian@example.com", "SebastianMitchell46!", "Student");
            await CreateUserWithRole("Amelia", "Walker", "amelia@example.com", "AmeliaWalker47!", "Lecturer");
            await CreateUserWithRole("Elijah", "Allen", "elijah@example.com", "ElijahAllen48!", "Student");
            await CreateUserWithRole("Lily", "Perez", "lily@example.com", "LilyPerez49!", "Lecturer");
            await CreateUserWithRole("Mason", "Hall", "mason@example.com", "MasonHall50!", "Student");
        }
    }
}
