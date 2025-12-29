using CMA.Application.DTOs;
using CMA.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CMA.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        IEnumerable<OrderDto> orders;

        if (User.IsInRole("Supervisor") || User.IsInRole("Manager"))
        {
            orders = await _orderService.GetAllAsync();
        }
        else if (User.IsInRole("Dealer"))
        {
            orders = await _orderService.GetByDealerAsync(userId!);
        }
        else if (User.IsInRole("SalesAgent"))
        {
            orders = await _orderService.GetBySalesAgentAsync(userId!);
        }
        else
        {
            return Forbid();
        }

        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order == null)
            return NotFound();

        return Ok(order);
    }

    [HttpPost]
    [Authorize(Roles = "Dealer")]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return Unauthorized();

        var order = await _orderService.CreateAsync(userId, dto);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    [HttpPut("{id}/assign-sales-agent")]
    [Authorize(Roles = "Dealer")]
    public async Task<IActionResult> AssignToSalesAgent(int id, [FromBody] AssignmentDto dto)
    {
        var order = await _orderService.AssignToSalesAgentAsync(id, dto.UserId);
        if (order == null)
            return NotFound();

        return Ok(order);
    }

    [HttpPut("{id}/assign-manager")]
    [Authorize(Roles = "SalesAgent")]
    public async Task<IActionResult> AssignToManager(int id, [FromBody] AssignmentDto dto)
    {
        var order = await _orderService.AssignToManagerAsync(id, dto.UserId);
        if (order == null)
            return NotFound();

        return Ok(order);
    }

    [HttpPut("{id}/status")]
    [Authorize(Roles = "Supervisor,Manager,SalesAgent")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] StatusUpdateDto dto)
    {
        var order = await _orderService.UpdateStatusAsync(id, dto.Status);
        if (order == null)
            return NotFound();

        return Ok(order);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Supervisor")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _orderService.DeleteAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}

public class AssignmentDto
{
    public string UserId { get; set; } = string.Empty;
}

public class StatusUpdateDto
{
    public string Status { get; set; } = string.Empty;
}
