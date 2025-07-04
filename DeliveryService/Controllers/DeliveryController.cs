using DeliveryService.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeliveryController : ControllerBase
{
    private readonly IDeliveryService _service;

    public DeliveryController(IDeliveryService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Get() =>
        Ok(await _service.GetDeliveriesAsync());
}