using CMA.Application.DTOs;

namespace CMA.Application.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllAsync();
    Task<IEnumerable<OrderDto>> GetByDealerAsync(string dealerId);
    Task<IEnumerable<OrderDto>> GetBySalesAgentAsync(string salesAgentId);
    Task<IEnumerable<OrderDto>> GetByManagerAsync(string managerId);
    Task<OrderDto?> GetByIdAsync(int id);
    Task<OrderDto> CreateAsync(string dealerId, CreateOrderDto dto);
    Task<OrderDto?> AssignToSalesAgentAsync(int orderId, string salesAgentId);
    Task<OrderDto?> AssignToManagerAsync(int orderId, string managerId);
    Task<OrderDto?> UpdateStatusAsync(int orderId, string status);
    Task<bool> DeleteAsync(int id);
}
