namespace CMA.Domain.Entities;

public class Employee : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser? User { get; set; }
    
    public string EmployeeId { get; set; } = string.Empty;
    public EmployeeType EmployeeType { get; set; }
    public DateTime HireDate { get; set; }
    public string Department { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public decimal BasicSalary { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? TerminationDate { get; set; }
    public string? Notes { get; set; }
    
    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    public ICollection<Salary> Salaries { get; set; } = new List<Salary>();
}

public enum EmployeeType
{
    Supervisor,
    SalesAgent,
    Manager,
    Driver,
    Accountant,
    Worker,
    Other
}
