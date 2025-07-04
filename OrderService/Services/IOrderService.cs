using OrderService.Contracts;
using OrderService.Db;

namespace OrderService.Services;

public interface IOrderService
{
    Task<Guid> CreateOrderAsync(OrderDto dto);
    Task<List<Order>> GetOrdersAsync();
}