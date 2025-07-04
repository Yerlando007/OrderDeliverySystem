using DeliveryService.Services;
using MassTransit;
using OrderDelivery.Contracts;

namespace DeliveryService.Consumers;

public class OrderCreatedConsumer : IConsumer<IOrderCreated>
{
    private readonly IDeliveryService _service;

    public OrderCreatedConsumer(IDeliveryService service) => _service = service;

    public async Task Consume(ConsumeContext<IOrderCreated> context)
    {
        var message = context.Message;
        await _service.CreateDeliveryAsync(message.Id, message.Product, message.Price);
    }
}