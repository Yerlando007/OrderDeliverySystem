using DeliveryService.Db;

namespace DeliveryService.Services;

public interface IDeliveryService
{
    Task CreateDeliveryAsync(Guid orderId, string product, decimal price);
    Task<List<Delivery>> GetDeliveriesAsync();
}