
using HRSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using HRSystem.WebAPI.Extensions;
using Serilog;
using HRSystem.Application.Mapping;

namespace HRSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //configuration db context 
            builder.Services.AddDbContext<HRSystemDBContext>(
                            option => option.UseSqlServer(builder.Configuration["ConnectionStrings:HRSystemDb"]));
            // add serilog service
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Warning()
                        .WriteTo.File("Files\\logs\\ExeptionFile.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
            //Add auto mapper here bro
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());


            // register service employee
            builder.Services.AddEmployeeServices();
            // register service Department
            builder.Services.AddDepartmentServices();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
