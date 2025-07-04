using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderDelivery.Contracts;
using OrderService.Contracts;
using OrderService.Db;

namespace OrderService.Services;

public class OrderService : IOrderService
{
    private readonly OrderDbContext _db;
    private readonly IPublishEndpoint _publish;

    public OrderService(OrderDbContext db, IPublishEndpoint publish)
    {
        _db = db;
        _publish = publish;
    }

    public async Task<Guid> CreateOrderAsync(OrderDto dto)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            Product = dto.Product,
            Price = dto.Price
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        await _publish.Publish<IOrderCreated>(new
        {
            order.Id,
            order.Product,
            order.Price
        });

        return order.Id;
    }

    public Task<List<Order>> GetOrdersAsync() =>
        _db.Orders.ToListAsync();
}