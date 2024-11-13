using HRSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HRSystem.Infrastructure.Data.Configurations
{
    public class SalaryTiersConfiguration : IEntityTypeConfiguration<SalaryTiers>
    {


        public void Configure(EntityTypeBuilder<SalaryTiers> builder)
        {
            builder.ToTable("SalaryTiers");
            builder.HasMany(e => e.Employees)
                .WithOne(s => s.SalaryTiers)
                .HasForeignKey(e => e.SalaryTiersId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
