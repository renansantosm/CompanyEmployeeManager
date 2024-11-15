using CompanyEmployeeManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyEmployeeManager.EntitiesConfiration;

internal class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(c => c.CompanyId);

        builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
        builder.Property(c => c.Phone).HasMaxLength(20).IsRequired();
        builder.Property(c => c.Email).HasMaxLength(80).IsRequired();

        builder.HasMany(e => e.Employees).WithOne(c => c.Company).IsRequired().OnDelete(DeleteBehavior.Restrict);
    }
}
