using Application.Abstractions.Services;
using Application.Services.ApplicationStatus;
using Application.Services.JobApplications;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

            services.AddServices();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IJobApplicationService, JobApplicationService>();
            services.AddScoped<IApplicationStatusService, ApplicationStatusService>();
            return services;
        }
    }
}
