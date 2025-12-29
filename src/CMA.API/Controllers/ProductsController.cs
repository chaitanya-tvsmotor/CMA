using CMA.Application.DTOs;
using CMA.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CMA.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var includePrice = User.Identity?.IsAuthenticated == true && 
                          (User.IsInRole("Dealer") || User.IsInRole("Supervisor") || 
                           User.IsInRole("Manager") || User.IsInRole("SalesAgent"));
        
        var products = await _productService.GetAllAsync(includePrice);
        return Ok(products);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var includePrice = User.Identity?.IsAuthenticated == true && 
                          (User.IsInRole("Dealer") || User.IsInRole("Supervisor") || 
                           User.IsInRole("Manager") || User.IsInRole("SalesAgent"));
        
        var product = await _productService.GetByIdAsync(id, includePrice);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    [Authorize(Roles = "Supervisor")]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        var product = await _productService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Supervisor")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
    {
        if (id != dto.Id)
            return BadRequest();

        var product = await _productService.UpdateAsync(dto);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Supervisor")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _productService.DeleteAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
