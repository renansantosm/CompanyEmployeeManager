using CompanyEmployeeManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyEmployeeManager.EntitiesConfiration;

internal class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.HasKey(p => p.PositionId);

        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Description).HasMaxLength(250);

        builder.HasMany(e => e.Employees).WithOne(p => p.Position).OnDelete(DeleteBehavior.Restrict).IsRequired();
    }
}
