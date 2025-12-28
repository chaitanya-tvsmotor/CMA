using CMA.Application.DTOs;

namespace CMA.Application.Interfaces;

public interface IPurchaseOrderService
{
    Task<IEnumerable<PurchaseOrderDto>> GetAllAsync();
    Task<IEnumerable<PurchaseOrderDto>> GetByManagerAsync(string managerId);
    Task<PurchaseOrderDto?> GetByIdAsync(int id);
    Task<PurchaseOrderDto> CreateAsync(string managerId, CreatePurchaseOrderDto dto);
    Task<PurchaseOrderDto?> UpdateStatusAsync(int poId, string status);
    Task<bool> DeleteAsync(int id);
}
