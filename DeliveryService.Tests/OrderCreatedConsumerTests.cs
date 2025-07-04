using DeliveryService.Consumers;
using DeliveryService.Db;
using DeliveryService.Tests.Fakes;
using FluentAssertions;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Moq;
using OrderDelivery.Contracts;

public class OrderCreatedConsumerTests
{
    private readonly DeliveryDbContext _context;
    private readonly OrderCreatedConsumer _consumer;

    public OrderCreatedConsumerTests()
    {
        var options = new DbContextOptionsBuilder<DeliveryDbContext>()
            .UseInMemoryDatabase("delivery_test_db")
            .Options;

        _context = new DeliveryDbContext(options);

        var service = new DeliveryService.Services.DeliveryService(_context);
        _consumer = new(service);
    }

    [Fact]
    public async Task Consume_ShouldSaveDelivery()
    {
        var fakeContext = new Mock<ConsumeContext<IOrderCreated>>();
        fakeContext.Setup(x => x.Message).Returns(new FakeOrderCreated
        {
            Id = Guid.NewGuid(),
            Product = "Pizza",
            Price = 50m
        });

        await _consumer.Consume(fakeContext.Object);

        var delivery = _context.Deliveries.FirstOrDefault();
        delivery.Should().NotBeNull();
        delivery!.Product.Should().Be("Pizza");
    }
}