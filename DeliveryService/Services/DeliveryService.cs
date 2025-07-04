using DeliveryService.Db;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Services;

public class DeliveryService : IDeliveryService
{
    private readonly DeliveryDbContext _db;

    public DeliveryService(DeliveryDbContext db) => _db = db;

    public async Task CreateDeliveryAsync(Guid orderId, string product, decimal price)
    {
        var delivery = new Delivery
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            Product = product,
            Status = "Created"
        };

        _db.Deliveries.Add(delivery);
        await _db.SaveChangesAsync();
    }

    public Task<List<Delivery>> GetDeliveriesAsync() => _db.Deliveries.ToListAsync();
}