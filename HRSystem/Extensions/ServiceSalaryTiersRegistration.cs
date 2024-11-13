using HRSystem.Application.UseCases.SalaryTiers;
using HRSystem.Application.IRepository;
using HRSystem.Application.Services.IServices;
using HRSystem.Application.Services;
using HRSystem.Infrastructure.Repositories;
using HRSystem.Application.UseCases.Employee;
using HRSystem.Infrastructure.Repositories.IRepository;

namespace HRSystem.WebAPI.Extensions
{
    public static class ServiceSalaryTiersRegistration
    {
        public static IServiceCollection AddSalaryTiersServices(this IServiceCollection services)
        {
            // Register commands and queries for salary tiers
            services.AddScoped<AddSalaryTiersCommand>();
            services.AddScoped<UpdateSalaryTiersCommand>();
            services.AddScoped<GetSalaryTiersQuery>();
            services.AddScoped<DeleteSalaryTiersCommand>();
            services.AddScoped<GetReportAboutSalary>();

            // Register repository and service for salary tiers
            services.AddScoped<ISalaryTiersRepository, SalaryTiersRepository>();
            services.AddScoped<ISalaryTiersService, SalaryTiersService>();

            return services;
        }
    }
}
