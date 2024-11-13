using HRSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using HRSystem.WebAPI.Extensions;
using Serilog;
using HRSystem.Application.Mapping;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HRSystem.WebAPI.Middlewares;
using Microsoft.OpenApi.Models;

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

                // Add JWT Authentication to Swagger
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
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
            builder.Services.AddAuthentication().AddJwtBearer(option =>
                                                  option.TokenValidationParameters = new()
                                                  {
                                                      ValidIssuer = builder.Configuration["Authentication:Issuer"],
                                                      ValidAudience = builder.Configuration["Authentication:Audience"],
                                                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretKey"])),
                                                      ValidateAudience = true,
                                                      ValidateIssuer = true,
                                                      ValidateIssuerSigningKey = true,
                                                  });
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("cors-policy", builder => builder
                                                    .WithOrigins("http://localhost:4200")
                                                    .AllowAnyMethod()
                                                    .AllowAnyHeader()
                                                    .AllowCredentials());
            });
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
            app.UseAuthentication();//here complate Authentication
            app.UseMiddleware<EmployeeNameValidationMiddleware>();
            app.UseCors("cors-policy");
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
