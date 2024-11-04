using HRSystem.Application.Interfaces;
using HRSystem.Application.UseCases.Department;
using HRSystem.Application.UseCases.Employee;
using HRSystem.Domain.Interfaces;
using HRSystem.Infrastructure.Repositories;

namespace HRSystem.WebAPI.Extensions
{
    public static class ServiceDepartmentRegistration
    {
        public static IServiceCollection AddDepartmentServices(this IServiceCollection services)
        {
            services.AddScoped<AddDepartmentCommand>();
            services.AddScoped<UpdateDepartmentCommand>();
            services.AddScoped<GetDepartmentQuery>();
            services.AddScoped<IDepartmentService, DepartmentRepository>();

            return services;
        }
    }
}
