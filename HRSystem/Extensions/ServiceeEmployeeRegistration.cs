using HRSystem.Application.UseCases.Employee;
using HRSystem.Application.IRepository;
using HRSystem.Infrastructure.Repositories;
using HRSystem.Application.Services.IServices;
using HRSystem.Application.Services;

namespace HRSystem.WebAPI.Extensions
{
    public static class ServiceEmployeeRegistration
    {
        public static IServiceCollection AddEmployeeServices(this IServiceCollection services)
        {
            services.AddScoped<AddEmployeeCommand>();
            services.AddScoped<UpdateEmployeeCommand>();
            services.AddScoped<GetEmployeeQuery>();
            services.AddScoped<DeleteEmployeeCommand>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}
