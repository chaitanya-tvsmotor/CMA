using CMA.Application.DTOs;
using CMA.Application.Interfaces;
using CMA.Domain.Entities;
using CMA.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CMA.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderDto>> GetAllAsync()
    {
        var orders = await _context.Orders
            .Include(o => o.Dealer)
            .Include(o => o.SalesAgent)
            .Include(o => o.Manager)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .ToListAsync();

        return orders.Select(MapToDto);
    }

    public async Task<IEnumerable<OrderDto>> GetByDealerAsync(string dealerId)
    {
        var orders = await _context.Orders
            .Include(o => o.Dealer)
            .Include(o => o.SalesAgent)
            .Include(o => o.Manager)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Where(o => o.DealerId == dealerId)
            .ToListAsync();

        return orders.Select(MapToDto);
    }

    public async Task<IEnumerable<OrderDto>> GetBySalesAgentAsync(string salesAgentId)
    {
        var orders = await _context.Orders
            .Include(o => o.Dealer)
            .Include(o => o.SalesAgent)
            .Include(o => o.Manager)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Where(o => o.SalesAgentId == salesAgentId)
            .ToListAsync();

        return orders.Select(MapToDto);
    }

    public async Task<IEnumerable<OrderDto>> GetByManagerAsync(string managerId)
    {
        var orders = await _context.Orders
            .Include(o => o.Dealer)
            .Include(o => o.SalesAgent)
            .Include(o => o.Manager)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Where(o => o.ManagerId == managerId)
            .ToListAsync();

        return orders.Select(MapToDto);
    }

    public async Task<OrderDto?> GetByIdAsync(int id)
    {
        var order = await _context.Orders
            .Include(o => o.Dealer)
            .Include(o => o.SalesAgent)
            .Include(o => o.Manager)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id);

        return order == null ? null : MapToDto(order);
    }

    public async Task<OrderDto> CreateAsync(string dealerId, CreateOrderDto dto)
    {
        var order = new Order
        {
            OrderNumber = GenerateOrderNumber(),
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Draft,
            DealerId = dealerId,
            Notes = dto.Notes,
            CreatedAt = DateTime.UtcNow,
            OrderItems = new List<OrderItem>()
        };

        decimal totalAmount = 0;
        foreach (var itemDto in dto.Items)
        {
            var product = await _context.Products.FindAsync(itemDto.ProductId);
            if (product == null) continue;

            var orderItem = new OrderItem
            {
                ProductId = itemDto.ProductId,
                Quantity = itemDto.Quantity,
                UnitPrice = product.Price,
                TotalPrice = product.Price * itemDto.Quantity,
                CreatedAt = DateTime.UtcNow
            };

            order.OrderItems.Add(orderItem);
            totalAmount += orderItem.TotalPrice;
        }

        order.TotalAmount = totalAmount;
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return (await GetByIdAsync(order.Id))!;
    }

    public async Task<OrderDto?> AssignToSalesAgentAsync(int orderId, string salesAgentId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null) return null;

        order.SalesAgentId = salesAgentId;
        order.Status = OrderStatus.PendingApproval;
        order.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return await GetByIdAsync(orderId);
    }

    public async Task<OrderDto?> AssignToManagerAsync(int orderId, string managerId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null) return null;

        order.ManagerId = managerId;
        order.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return await GetByIdAsync(orderId);
    }

    public async Task<OrderDto?> UpdateStatusAsync(int orderId, string status)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null) return null;

        if (Enum.TryParse<OrderStatus>(status, out var orderStatus))
        {
            order.Status = orderStatus;
            order.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        return await GetByIdAsync(orderId);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return false;

        order.IsDeleted = true;
        order.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    private string GenerateOrderNumber()
    {
        return $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";
    }

    private OrderDto MapToDto(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            OrderDate = order.OrderDate,
            Status = order.Status.ToString(),
            TotalAmount = order.TotalAmount,
            Notes = order.Notes,
            DealerId = order.DealerId,
            DealerName = order.Dealer != null ? $"{order.Dealer.FirstName} {order.Dealer.LastName}" : null,
            SalesAgentId = order.SalesAgentId,
            SalesAgentName = order.SalesAgent != null ? $"{order.SalesAgent.FirstName} {order.SalesAgent.LastName}" : null,
            ManagerId = order.ManagerId,
            ManagerName = order.Manager != null ? $"{order.Manager.FirstName} {order.Manager.LastName}" : null,
            Items = order.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                ProductId = oi.ProductId,
                ProductName = oi.Product?.Name,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice,
                TotalPrice = oi.TotalPrice
            }).ToList()
        };
    }
}
