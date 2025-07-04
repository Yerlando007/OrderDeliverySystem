using OrderDelivery.Contracts;

namespace DeliveryService.Tests.Fakes;

public class FakeOrderCreated : IOrderCreated
{
    public Guid Id { get; set; }
    public string Product { get; set; } = string.Empty;
    public decimal Price { get; set; }
}