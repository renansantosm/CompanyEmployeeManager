using CompanyEmployeeManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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
        mb.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Address
        mb.Entity<Address>().HasData(
            new Address { AddressId = 1, Street = "Main St", Number = "123", City = "Metropolis", State = "NY", Country = "USA", PostalCode = "12345" },
            new Address { AddressId = 2, Street = "Elm St", Number = "456", City = "Gotham", State = "NJ", Country = "USA", PostalCode = "54321" },
            new Address { AddressId = 3, Street = "Oak St", Number = "789", City = "Star City", State = "CA", Country = "USA", PostalCode = "67890" }
        );

        // Company
        mb.Entity<Company>().HasData(
            new Company { CompanyId = 1, Name = "TechCorp", Phone = "123-456-7890", Email = "info@techcorp.com", AddressId = 1 },
            new Company { CompanyId = 2, Name = "Wayne Enterprises", Phone = "098-765-4321", Email = "info@wayne.com", AddressId = 2 },
            new Company { CompanyId = 3, Name = "Queen Consolidated", Phone = "321-654-9870", Email = "info@queen.com", AddressId = 3 }
        );

        // Position
        mb.Entity<Position>().HasData(
            new Position { PositionId = 1, Name = "Software Engineer", Description = "Responsible for developing software." },
            new Position { PositionId = 2, Name = "Project Manager", Description = "Oversees projects and teams." },
            new Position { PositionId = 3, Name = "Data Analyst", Description = "Analyzes and interprets complex data." },
            new Position { PositionId = 4, Name = "HR Specialist", Description = "Handles human resources tasks." },
            new Position { PositionId = 5, Name = "DevOps Engineer", Description = "Manages infrastructure and deployments." },
            new Position { PositionId = 6, Name = "Marketing Manager", Description = "Oversees marketing strategies." }
        );

        // Employee
        mb.Entity<Employee>().HasData(
            new Employee { EmployeeId = 1, Name = "John Doe", Age = 30, CompanyId = 1, PositionId = 1 },
            new Employee { EmployeeId = 2, Name = "Jane Smith", Age = 28, CompanyId = 1, PositionId = 2 },
            new Employee { EmployeeId = 3, Name = "Bruce Wayne", Age = 35, CompanyId = 2, PositionId = 3 },
            new Employee { EmployeeId = 4, Name = "Clark Kent", Age = 32, CompanyId = 2, PositionId = 1 },
            new Employee { EmployeeId = 5, Name = "Diana Prince", Age = 30, CompanyId = 2, PositionId = 4 },
            new Employee { EmployeeId = 6, Name = "Oliver Queen", Age = 34, CompanyId = 3, PositionId = 5 },
            new Employee { EmployeeId = 7, Name = "Felicity Smoak", Age = 29, CompanyId = 3, PositionId = 1 },
            new Employee { EmployeeId = 8, Name = "Barry Allen", Age = 27, CompanyId = 1, PositionId = 3 },
            new Employee { EmployeeId = 9, Name = "Hal Jordan", Age = 31, CompanyId = 3, PositionId = 2 },
            new Employee { EmployeeId = 10, Name = "Arthur Curry", Age = 33, CompanyId = 2, PositionId = 6 }
        );
    }
}
