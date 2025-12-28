using CMA.Application.DTOs;

namespace CMA.Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync(bool includePrice);
    Task<ProductDto?> GetByIdAsync(int id, bool includePrice);
    Task<ProductDto> CreateAsync(CreateProductDto dto);
    Task<ProductDto?> UpdateAsync(UpdateProductDto dto);
    Task<bool> DeleteAsync(int id);
}
