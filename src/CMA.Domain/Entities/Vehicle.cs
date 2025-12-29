namespace CMA.Domain.Entities;

public class Vehicle : BaseEntity
{
    public string VehicleNumber { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public VehicleStatus Status { get; set; }
    public string? Notes { get; set; }
    
    // Assigned to employee
    public string? AssignedToId { get; set; }
    public ApplicationUser? AssignedTo { get; set; }
}

public enum VehicleStatus
{
    Available,
    InUse,
    Maintenance,
    OutOfService
}
