using CMA.Application.DTOs;
using CMA.Application.Interfaces;
using CMA.Domain.Entities;
using CMA.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CMA.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync(bool includePrice)
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .ToListAsync();
        return products.Select(p => MapToDto(p, includePrice));
    }

    public async Task<ProductDto?> GetByIdAsync(int id, bool includePrice)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.Id == id);
        return product == null ? null : MapToDto(product, includePrice);
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            ImageUrl = dto.ImageUrl,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity,
            CategoryId = dto.CategoryId,
            BrandId = dto.BrandId,
            Specifications = dto.Specifications,
            SKU = dto.SKU,
            Barcode = dto.Barcode,
            Length = dto.Length,
            Width = dto.Width,
            Height = dto.Height,
            Weight = dto.Weight,
            CreatedAt = DateTime.UtcNow
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return (await GetByIdAsync(product.Id, true))!;
    }

    public async Task<ProductDto?> UpdateAsync(UpdateProductDto dto)
    {
        var product = await _context.Products.FindAsync(dto.Id);
        if (product == null) return null;

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.ImageUrl = dto.ImageUrl;
        product.Price = dto.Price;
        product.StockQuantity = dto.StockQuantity;
        product.CategoryId = dto.CategoryId;
        product.BrandId = dto.BrandId;
        product.Specifications = dto.Specifications;
        product.SKU = dto.SKU;
        product.Barcode = dto.Barcode;
        product.Length = dto.Length;
        product.Width = dto.Width;
        product.Height = dto.Height;
        product.Weight = dto.Weight;
        product.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return await GetByIdAsync(product.Id, true);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        product.IsDeleted = true;
        product.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    private ProductDto MapToDto(Product product, bool includePrice)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            Price = includePrice ? product.Price : null,
            StockQuantity = product.StockQuantity,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name,
            BrandId = product.BrandId,
            BrandName = product.Brand?.Name,
            Specifications = product.Specifications,
            SKU = product.SKU,
            Barcode = product.Barcode,
            Length = product.Length,
            Width = product.Width,
            Height = product.Height,
            Weight = product.Weight
        };
    }
}
