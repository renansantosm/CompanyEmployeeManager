using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyEmployeeManager.Migrations
{
    /// <inheritdoc />
    public partial class SeedDB : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            // Address
            mb.Sql("INSERT INTO Addresses (Street, Number, City, State, Country, PostalCode) " +
                   "VALUES ('Main St', '123', 'Metropolis', 'NY', 'USA', '12345')");
            mb.Sql("INSERT INTO Addresses (Street, Number, City, State, Country, PostalCode) " +
                   "VALUES ('Elm St', '456', 'Gotham', 'NJ', 'USA', '54321')");
            mb.Sql("INSERT INTO Addresses (Street, Number, City, State, Country, PostalCode) " +
                   "VALUES ('Oak St', '789', 'Star City', 'CA', 'USA', '67890')");

            // Company
            mb.Sql("INSERT INTO Companies (Name, Phone, Email, AddressId) " +
                   "VALUES ('TechCorp', '123-456-7890', 'info@techcorp.com', 1)");
            mb.Sql("INSERT INTO Companies (Name, Phone, Email, AddressId) " +
                   "VALUES ('Wayne Enterprises', '098-765-4321', 'info@wayne.com', 2)");
            mb.Sql("INSERT INTO Companies (Name, Phone, Email, AddressId) " +
                   "VALUES ('Queen Consolidated', '321-654-9870', 'info@queen.com', 3)");

            // Position
            mb.Sql("INSERT INTO Positions (Name, Description) VALUES ('Software Engineer', 'Responsible for developing software.')");
            mb.Sql("INSERT INTO Positions (Name, Description) VALUES ('Project Manager', 'Oversees projects and teams.')");
            mb.Sql("INSERT INTO Positions (Name, Description) VALUES ('Data Analyst', 'Analyzes and interprets complex data.')");
            mb.Sql("INSERT INTO Positions (Name, Description) VALUES ('HR Specialist', 'Handles human resources tasks.')");
            mb.Sql("INSERT INTO Positions (Name, Description) VALUES ('DevOps Engineer', 'Manages infrastructure and deployments.')");
            mb.Sql("INSERT INTO Positions (Name, Description) VALUES ('Marketing Manager', 'Oversees marketing strategies.')");

            // Employee
            mb.Sql("INSERT INTO Employees (Name, Age, CompanyId, PositionId) VALUES ('John Doe', 30, 1, 1)");
            mb.Sql("INSERT INTO Employees (Name, Age, CompanyId, PositionId) VALUES ('Jane Smith', 28, 1, 2)");
            mb.Sql("INSERT INTO Employees (Name, Age, CompanyId, PositionId) VALUES ('Bruce Wayne', 35, 2, 3)");
            mb.Sql("INSERT INTO Employees (Name, Age, CompanyId, PositionId) VALUES ('Clark Kent', 32, 2, 1)");
            mb.Sql("INSERT INTO Employees (Name, Age, CompanyId, PositionId) VALUES ('Diana Prince', 30, 2, 4)");
            mb.Sql("INSERT INTO Employees (Name, Age, CompanyId, PositionId) VALUES ('Oliver Queen', 34, 3, 5)");
            mb.Sql("INSERT INTO Employees (Name, Age, CompanyId, PositionId) VALUES ('Felicity Smoak', 29, 3, 1)");
            mb.Sql("INSERT INTO Employees (Name, Age, CompanyId, PositionId) VALUES ('Barry Allen', 27, 1, 3)");
            mb.Sql("INSERT INTO Employees (Name, Age, CompanyId, PositionId) VALUES ('Hal Jordan', 31, 3, 2)");
            mb.Sql("INSERT INTO Employees (Name, Age, CompanyId, PositionId) VALUES ('Arthur Curry', 33, 2, 6)");
        }

        protected override void Down(MigrationBuilder mb) 
        {
            mb.Sql("Remove from Addresses");
            mb.Sql("Remove from Companies");
            mb.Sql("Remove from Positions");
            mb.Sql("Remove from Employees");
        }
    }
}
