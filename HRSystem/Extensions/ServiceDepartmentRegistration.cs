using HRSystem.Application.Services;
using HRSystem.Application.Services.IServices;
using HRSystem.Application.UseCases.Department;
using HRSystem.Infrastructure.Repositories.IRepository;
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
            services.AddScoped<DeleteDepartmentCommand>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDepartmentService, DepartmentService>();

            return services;
        }
    }
}
