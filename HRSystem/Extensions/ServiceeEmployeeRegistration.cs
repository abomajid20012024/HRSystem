using HRSystem.Application.UseCases.Employee;
using HRSystem.Domain.Interfaces;
using HRSystem.Infrastructure.Repositories;

namespace HRSystem.WebAPI.Extensions
{
    public static class ServiceEmployeeRegistration
    {
        public static IServiceCollection AddEmployeeServices(this IServiceCollection services)
        {
            services.AddScoped<AddEmployeeCommand>();
            services.AddScoped<UpdateEmployeeCommand>();
            services.AddScoped<GetEmployeeQuery>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
