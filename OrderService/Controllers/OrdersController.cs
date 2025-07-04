using Microsoft.AspNetCore.Mvc;
using OrderService.Contracts;
using OrderService.Services;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService) =>
        _orderService = orderService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderDto dto)
    {
        var id = await _orderService.CreateOrderAsync(dto);
        return Ok(new { id });
    }

    [HttpGet]
    public async Task<IActionResult> Get() =>
        Ok(await _orderService.GetOrdersAsync());
}