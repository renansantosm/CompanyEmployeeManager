using System.Collections.ObjectModel;

namespace CompanyEmployeeManager.Models;

public class Company
{
    public Company()
    {
        Employees = new Collection<Employee>();
    }

    public int CompanyId { get; set; }
    public required string Name { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public ICollection<Employee> Employees { get; set; }

    public int AddressId { get; set; }
    public Address? Address { get; set; }
}
