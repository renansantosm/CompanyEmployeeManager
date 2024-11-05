using CompanyEmployeeManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployeeManager.Context;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);

        // Company
        mb.Entity<Company>()
            .HasKey(c => c.CompanyId);

        mb.Entity<Company>()
            .Property(c => c.Name)
                .HasMaxLength(100)
                    .IsRequired();

        mb.Entity<Company>()
            .Property(c => c.Phone)
                .HasMaxLength(20)
                    .IsRequired();

        mb.Entity<Company>()
            .Property(c => c.Email)
                .HasMaxLength(80)
                    .IsRequired();


        mb.Entity<Company>().HasMany(e => e.Employees)
            .WithOne(c => c.Company)
                .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

        // Employee
        mb.Entity<Employee>()
            .ToTable(e => e.HasCheckConstraint("CHK_Age", "Age >= 18 AND Age <= 65"));

        mb.Entity<Employee>()
            .HasKey(e => e.EmployeeId);

        mb.Entity<Employee>()
            .Property(e => e.Name)
                .HasMaxLength(100)
                    .IsRequired();

        mb.Entity<Employee>()
            .Property(e => e.Age)
                .IsRequired();

        // Position
        mb.Entity<Position>()
            .HasKey(p => p.PositionId);

        mb.Entity<Position>()
            .Property(p => p.Name)
                .HasMaxLength(100)
                    .IsRequired();

        mb.Entity<Position>()
            .Property(p => p.Description)
                .HasMaxLength(250);

        mb.Entity<Position>()
            .HasMany(e => e.Employees)
                .WithOne(p => p.Position)
                    .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

        // Address
        mb.Entity<Address>().
            HasKey(a => a.AddressId);

        mb.Entity<Address>().
            Property(a => a.Street)
                .HasMaxLength(120)
                    .IsRequired();

        mb.Entity<Address>().
            Property(a => a.Number)
                .HasMaxLength(10)
                    .IsRequired();

        mb.Entity<Address>()
            .Property(a => a.City)
                .HasMaxLength(50)
                    .IsRequired();

        mb.Entity<Address>()
            .Property(a => a.State)
                .HasMaxLength(50)
                    .IsRequired();

        mb.Entity<Address>()
            .Property(a => a.Country)
                .HasMaxLength(50)
                    .IsRequired();


        mb.Entity<Address>()
            .Property(a => a.PostalCode)
                .HasMaxLength(12)
                    .IsRequired();

        mb.Entity<Address>()
            .HasMany(c => c.Companies)
                .WithOne(a => a.Address)
                    .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
    }
}
