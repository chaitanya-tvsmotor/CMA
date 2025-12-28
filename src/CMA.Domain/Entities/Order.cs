namespace CMA.Domain.Entities;

public class Order : BaseEntity
{
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Notes { get; set; }
    
    // User relationships
    public string DealerId { get; set; } = string.Empty;
    public ApplicationUser? Dealer { get; set; }
    
    public string? SalesAgentId { get; set; }
    public ApplicationUser? SalesAgent { get; set; }
    
    public string? ManagerId { get; set; }
    public ApplicationUser? Manager { get; set; }
    
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

public enum OrderStatus
{
    Draft,
    PendingApproval,
    Approved,
    Rejected,
    Processing,
    Completed,
    Cancelled
}
