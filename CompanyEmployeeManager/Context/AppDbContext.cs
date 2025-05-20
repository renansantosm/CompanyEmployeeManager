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
            new Address { AddressId = 1, Street = "Rua das Flores", Number = "123", City = "São Paulo", State = "SP", Country = "Brasil", PostalCode = "01001-000" },
            new Address { AddressId = 2, Street = "Avenida Brasil", Number = "456", City = "Rio de Janeiro", State = "RJ", Country = "Brasil", PostalCode = "20040-002" },
            new Address { AddressId = 3, Street = "Rua XV de Novembro", Number = "789", City = "Curitiba", State = "PR", Country = "Brasil", PostalCode = "80020-310" }
        );

        // Company
        mb.Entity<Company>().HasData(
            new Company { CompanyId = 1, Name = "InfoTech Brasil", Phone = "(11) 91234-5678", Email = "contato@infotech.com.br", AddressId = 1 },
            new Company { CompanyId = 2, Name = "Soluciona TI", Phone = "(21) 99876-5432", Email = "suporte@soluciona.com.br", AddressId = 2 },
            new Company { CompanyId = 3, Name = "Paraná Systems", Phone = "(41) 98765-4321", Email = "atendimento@paranasy.com.br", AddressId = 3 }
        );

        // Position
        mb.Entity<Position>().HasData(
            new Position { PositionId = 1, Name = "Desenvolvedor Back-End", Description = "Responsável pelo desenvolvimento do servidor e banco de dados." },
            new Position { PositionId = 2, Name = "Gerente de Projetos", Description = "Gerencia projetos e equipes de desenvolvimento." },
            new Position { PositionId = 3, Name = "Analista de Dados", Description = "Analisa e interpreta dados para decisões estratégicas." },
            new Position { PositionId = 4, Name = "Recursos Humanos", Description = "Cuida dos processos relacionados a pessoas." },
            new Position { PositionId = 5, Name = "Engenheiro DevOps", Description = "Automatiza e gerencia processos de infraestrutura." },
            new Position { PositionId = 6, Name = "Coordenador de Marketing", Description = "Elabora e coordena ações de marketing." }
        );

        // Employee
        mb.Entity<Employee>().HasData(
            new Employee { EmployeeId = 1, Name = "Ana Souza", Age = 29, CompanyId = 1, PositionId = 1 },
            new Employee { EmployeeId = 2, Name = "Carlos Lima", Age = 35, CompanyId = 1, PositionId = 2 },
            new Employee { EmployeeId = 3, Name = "Beatriz Ferreira", Age = 31, CompanyId = 2, PositionId = 3 },
            new Employee { EmployeeId = 4, Name = "João Mendes", Age = 27, CompanyId = 2, PositionId = 1 },
            new Employee { EmployeeId = 5, Name = "Mariana Oliveira", Age = 32, CompanyId = 2, PositionId = 4 },
            new Employee { EmployeeId = 6, Name = "Rafael Costa", Age = 33, CompanyId = 3, PositionId = 5 },
            new Employee { EmployeeId = 7, Name = "Juliana Martins", Age = 28, CompanyId = 3, PositionId = 1 },
            new Employee { EmployeeId = 8, Name = "Pedro Henrique", Age = 26, CompanyId = 1, PositionId = 3 },
            new Employee { EmployeeId = 9, Name = "Fernanda Alves", Age = 30, CompanyId = 3, PositionId = 2 },
            new Employee { EmployeeId = 10, Name = "Lucas Ribeiro", Age = 34, CompanyId = 2, PositionId = 6 }
        );
    }
}
