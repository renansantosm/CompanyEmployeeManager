using CompanyEmployeeManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyEmployeeManager.EntitiesConfiration;

internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable(e => e.HasCheckConstraint("CHK_Age", "Age >= 18 AND Age <= 65"));

        builder.HasKey(e => e.EmployeeId);

        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
        builder.Property(e => e.Age).IsRequired();
    }
}
