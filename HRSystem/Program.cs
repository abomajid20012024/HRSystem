using HRSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using HRSystem.WebAPI.Extensions;
using Serilog;
using HRSystem.Application.Mapping;
using System.Reflection;

namespace HRSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Serilog for logging
            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Warning()
                            .WriteTo.File("Files/logs/ExceptionFile.txt", rollingInterval: RollingInterval.Day)
                            .CreateLogger();
            builder.Host.UseSerilog(); // Register Serilog

            // Add services to the container
            builder.Services.AddControllers();

            // Enable Swagger with XML comments for API documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                // Configure XML comments path for Swagger
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            // Configure database context with SQL Server
            builder.Services.AddDbContext<HRSystemDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("HRSystemDb")));

            // Configure AutoMapper with MappingProfile
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

            // Register custom services for Dependency Injection
            builder.Services.AddEmployeeServices();
            builder.Services.AddDepartmentServices();
            builder.Services.AddSalaryTiersServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HRSystem API v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
