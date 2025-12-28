namespace CMA.Domain.Entities;

public class PurchaseOrder : BaseEntity
{
    public string PONumber { get; set; } = string.Empty;
    public DateTime PODate { get; set; }
    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    public PurchaseOrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime? ExpectedDeliveryDate { get; set; }
    public string? Notes { get; set; }
    
    public string ManagerId { get; set; } = string.Empty;
    public ApplicationUser? Manager { get; set; }
    
    public ICollection<PurchaseOrderItem> Items { get; set; } = new List<PurchaseOrderItem>();
}

public enum PurchaseOrderStatus
{
    Draft,
    Submitted,
    Approved,
    Rejected,
    Received,
    Cancelled
}
