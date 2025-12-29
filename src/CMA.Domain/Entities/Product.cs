namespace CMA.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string? Specifications { get; set; }
    
    // Category and Brand relationships
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public int BrandId { get; set; }
    public Brand? Brand { get; set; }
    
    // Dimensions
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public string? DimensionUnit { get; set; } = "cm";
    public decimal? Weight { get; set; }
    public string? WeightUnit { get; set; } = "kg";
    
    // SKU and Barcode
    public string? SKU { get; set; }
    public string? Barcode { get; set; }
    
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
