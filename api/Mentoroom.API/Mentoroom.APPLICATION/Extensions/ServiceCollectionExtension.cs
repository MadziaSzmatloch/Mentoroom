using FluentValidation;
using Mentoroom.APPLICATION.Services.AuthServices;
using Mentoroom.APPLICATION.Validators;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Mentoroom.APPLICATION.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddValidatorsFromAssemblyContaining<CourseValidator>();
        }
    }
}
