using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ECommerce.DTOs;
using ECommerce.Services;

namespace ECommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly OrderService _service;
    public OrdersController(OrderService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetMyOrders()
        => Ok(await _service.GetUserOrders(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value));

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest req)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value;
        return Ok(await _service.CreateOrder(userId, req));
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllOrders()
        => Ok(await _service.GetAllOrders());

    [HttpPut("{id}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateOrderStatusRequest req)
    {
        var updated = await _service.UpdateStatus(id, req.Status);
        return updated is null ? NotFound() : Ok(updated);
    }
}
