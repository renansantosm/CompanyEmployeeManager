using CompanyEmployeeManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyEmployeeManager.EntitiesConfiration;

internal class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(a => a.AddressId);

        builder.Property(a => a.Street).HasMaxLength(120).IsRequired();
        builder.Property(a => a.Number).HasMaxLength(10).IsRequired();
        builder.Property(a => a.City).HasMaxLength(50).IsRequired();
        builder.Property(a => a.State).HasMaxLength(50).IsRequired();
        builder.Property(a => a.Country).HasMaxLength(50).IsRequired();
        builder.Property(a => a.PostalCode).HasMaxLength(12).IsRequired();

        builder.HasMany(c => c.Companies).WithOne(a => a.Address).OnDelete(DeleteBehavior.Restrict).IsRequired();
    }
}
