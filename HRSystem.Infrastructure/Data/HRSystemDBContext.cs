using HRSystem.Domain.Entities;
using HRSystem.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.Infrastructure.Data
{
    public class HRSystemDBContext : DbContext
    {
        public HRSystemDBContext(DbContextOptions<HRSystemDBContext> options) : base(options)
        {

        }
        public HRSystemDBContext()
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<SalaryTiers> SalaryTiers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new SalaryTiersConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Method intentionally left empty.
        }
    }
}
