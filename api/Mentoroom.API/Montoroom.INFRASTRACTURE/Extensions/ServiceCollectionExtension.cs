using Azure.Storage.Blobs;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;
using Mentoroom.DOMAIN.Interfaces.Tags;
using Mentoroom.DOMAIN.Models;
using Mentoroom.INFRASTRACTURE.Persistence;
using Mentoroom.INFRASTRACTURE.Repositories;
using Mentoroom.INFRASTRACTURE.Repositories.Shared;
using Mentoroom.INFRASTRACTURE.Repositories.StudentRepositories;
using Mentoroom.INFRASTRACTURE.Repositories.Tags;
using Mentoroom.INFRASTRACTURE.Seeders;
using Mentoroom.INFRASTRACTURE.Seeders.Authors;
using Mentoroom.INFRASTRACTURE.Seeders.Shared;
using Mentoroom.INFRASTRACTURE.Seeders.Shared.Assignments;
using Mentoroom.INFRASTRACTURE.Seeders.StudentSeeders;
using Mentoroom.INFRASTRACTURE.Seeders.Tags;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mentoroom.INFRASTRACTURE.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastracture(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));

            TokenSettings settings = configuration.GetSection("JwtSettings").Get<TokenSettings>() ?? default!;
            services.AddSingleton(settings);

            services.InjectRepositories();
            services.InjectSeeders();

            services.AddExceptionHandler<GlobalExceptionHandler>();
        }

        public static void AddJwtIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<AppUser>()
                .AddRoles<IdentityRole>()
                .AddSignInManager()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<AppUser>>("REFRESHTOKENPROVIDER");


            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromSeconds(double.Parse(configuration.GetSection("JwtSettings:RefreshTokenExpireSeconds").Value!));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidIssuer = configuration.GetSection("JwtSettings:Issuer").Value,
                    ValidAudience = configuration.GetSection("JwtSettings:Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings:SecretKey").Value!)),
                    ClockSkew = TimeSpan.FromSeconds(0)
                };
            });
        }

        public static void AddFileRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(configuration["AzureBlobStorage:ConnectionString"]);
            });
            services.AddScoped<IFileRepository>(provider =>
            {
                var blobServiceClient = provider.GetRequiredService<BlobServiceClient>();
                var filesContainer = blobServiceClient.GetBlobContainerClient("files");
                return new FileRepository(filesContainer);
            });
        }

        public static async Task AddSeeders(this IServiceScope scope)
        {
            await scope.ServiceProvider.GetRequiredService<AuthSeeder>().Seed();
            await scope.ServiceProvider.GetRequiredService<LecturerSeeder>().Seed();
            await scope.ServiceProvider.GetRequiredService<StudentSeeder>().Seed();
            await scope.ServiceProvider.GetRequiredService<DegreeSeeder>().Seed();
            await scope.ServiceProvider.GetRequiredService<YearSeeder>().Seed();
            await scope.ServiceProvider.GetRequiredService<SemesterSeeder>().Seed();
            await scope.ServiceProvider.GetRequiredService<DepartmentSeeder>().Seed();
            await scope.ServiceProvider.GetRequiredService<MajorsSeeder>().Seed();
            await scope.ServiceProvider.GetRequiredService<CourseSeeder>().Seed();
            await scope.ServiceProvider.GetRequiredService<StudentCourseSeeder>().Seed();
            await scope.ServiceProvider.GetRequiredService<AssignmentSeeder>().Seed();
            await scope.ServiceProvider.GetRequiredService<CourseCoAuthorSeeder>().Seed();
            await scope.ServiceProvider.GetRequiredService<AssignmentAttachmentSeeder>().Seed();
            await scope.ServiceProvider.GetRequiredService<AssignmentFilesSeeder>().Seed();
            await scope.ServiceProvider.GetRequiredService<StudentAssignmentSeeder>().Seed();
            await scope.ServiceProvider.GetRequiredService<StudentAssignmentFilesSeeder>().Seed();
        }

        private static void InjectSeeders(this IServiceCollection services)
        {
            services.AddScoped<AuthSeeder>();
            services.AddScoped<LecturerSeeder>();
            services.AddScoped<StudentSeeder>();
            services.AddScoped<DegreeSeeder>();
            services.AddScoped<YearSeeder>();
            services.AddScoped<SemesterSeeder>();
            services.AddScoped<DepartmentSeeder>();
            services.AddScoped<MajorsSeeder>();
            services.AddScoped<CourseSeeder>();
            services.AddScoped<StudentCourseSeeder>();
            services.AddScoped<AssignmentSeeder>();
            services.AddScoped<CourseCoAuthorSeeder>();
            services.AddScoped<AssignmentAttachmentSeeder>();
            services.AddScoped<AssignmentFilesSeeder>();
            services.AddScoped<StudentAssignmentSeeder>();
            services.AddScoped<StudentAssignmentFilesSeeder>();
        }

        private static void InjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccessCodesRepository, AccessCodesRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IMajorRepository, MajorRepository>();
            services.AddScoped<IDegreeRepository, DegreeRepository>();
            services.AddScoped<IYearRepository, YearRepository>();
            services.AddScoped<ISemesterRepository, SemesterRepository>();
            services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            services.AddScoped<IStudentCourseRepository, StudentCourseRepository>();
            services.AddScoped<ICourseCoAuthorRepository, CourseCoAuthorRepository>();
            services.AddScoped<IAssignmentFileRepository, AssignmentFileRepository>();
            services.AddScoped<IStudentAssignmentRepository, StudentAssignmentRepository>();
            services.AddScoped<IStudentAssignmentFileRepository, StudentAssignmentFileRepository>();
            services.AddScoped<IAssignmentAttachmentRepository, AssignmentAttachmentRepository>();
        }
    }
}
