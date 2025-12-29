namespace CMA.Application.DTOs;

public class PurchaseOrderDto
{
    public int Id { get; set; }
    public string PONumber { get; set; } = string.Empty;
    public DateTime PODate { get; set; }
    public int SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public DateTime? ExpectedDeliveryDate { get; set; }
    public string? Notes { get; set; }
    public string ManagerId { get; set; } = string.Empty;
    public string? ManagerName { get; set; }
    public List<PurchaseOrderItemDto> Items { get; set; } = new();
}

public class CreatePurchaseOrderDto
{
    public int SupplierId { get; set; }
    public DateTime? ExpectedDeliveryDate { get; set; }
    public string? Notes { get; set; }
    public List<CreatePurchaseOrderItemDto> Items { get; set; } = new();
}

public class PurchaseOrderItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}

public class CreatePurchaseOrderItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
