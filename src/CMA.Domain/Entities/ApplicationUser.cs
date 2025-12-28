using Microsoft.AspNetCore.Identity;

namespace CMA.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Dealer-specific fields
    public string? DealershipName { get; set; }
    public string? LicenseNumber { get; set; }
    
    // Navigation properties
    public Employee? EmployeeProfile { get; set; }
    public ICollection<Order> DealerOrders { get; set; } = new List<Order>();
    public ICollection<Order> SalesAgentOrders { get; set; } = new List<Order>();
    public ICollection<Order> ManagerOrders { get; set; } = new List<Order>();
    public ICollection<Vehicle> AssignedVehicles { get; set; } = new List<Vehicle>();
    public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
