namespace CMA.Domain.Entities;

public class Salary : BaseEntity
{
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal Allowances { get; set; }
    public decimal Bonus { get; set; }
    public decimal Deductions { get; set; }
    public decimal NetSalary { get; set; }
    public DateTime PaymentDate { get; set; }
    public SalaryStatus Status { get; set; }
    public string? Notes { get; set; }
    
    // Breakdown
    public decimal? HousingAllowance { get; set; }
    public decimal? TransportAllowance { get; set; }
    public decimal? MedicalAllowance { get; set; }
    public decimal? TaxDeduction { get; set; }
    public decimal? LoanDeduction { get; set; }
    public decimal? OvertimePay { get; set; }
}

public enum SalaryStatus
{
    Pending,
    Processed,
    Paid,
    OnHold,
    Cancelled
}
