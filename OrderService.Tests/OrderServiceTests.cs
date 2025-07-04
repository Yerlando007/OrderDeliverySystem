using FluentAssertions;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Moq;
using OrderDelivery.Contracts;
using OrderService.Contracts;
using OrderService.Db;
using OrderService.Services;

namespace OrderService.Tests
{
    public class OrderServiceTests
    {
        private readonly OrderDbContext _dbContext;
        private readonly Mock<IPublishEndpoint> _publishEndpointMock;
        private readonly IOrderService _orderService;

        public OrderServiceTests()
        {
            var options = new DbContextOptionsBuilder<OrderDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dbContext = new OrderDbContext(options);
            _publishEndpointMock = new Mock<IPublishEndpoint>();
            _orderService = new Services.OrderService(_dbContext, _publishEndpointMock.Object);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldSaveOrder_AndPublishEvent()
        {
            // Arrange
            var dto = new OrderDto { Product = "Test", Price = 123 };
            object? publishedEvent = null;

            _publishEndpointMock
                .Setup(x => x.Publish<IOrderCreated>(It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .Callback<object, CancellationToken>((msg, _) => publishedEvent = msg);

            // Act
            var id = await _orderService.CreateOrderAsync(dto);

            // Assert
            var order = await _dbContext.Orders.FindAsync(id);
            order.Should().NotBeNull();
            order!.Product.Should().Be("Test");
            order.Price.Should().Be(123);

            publishedEvent.Should().NotBeNull("Publish should be called with correct message");

            var productProp = publishedEvent!.GetType().GetProperty("Product")?.GetValue(publishedEvent)?.ToString();
            var priceProp = publishedEvent!.GetType().GetProperty("Price")?.GetValue(publishedEvent);
            var idProp = publishedEvent!.GetType().GetProperty("Id")?.GetValue(publishedEvent);

            productProp.Should().Be("Test");
            priceProp.Should().Be(123m);
            idProp.Should().Be(id);
        }

        [Fact]
        public async Task GetOrdersAsync_ShouldReturnAllOrders()
        {
            // Arrange
            _dbContext.Orders.AddRange(
                new Order { Id = Guid.NewGuid(), Product = "Product1", Price = 100 },
                new Order { Id = Guid.NewGuid(), Product = "Product2", Price = 200 }
            );
            await _dbContext.SaveChangesAsync();

            // Act
            var orders = await _orderService.GetOrdersAsync();

            // Assert
            orders.Should().HaveCount(2);
            orders.Should().Contain(o => o.Product == "Product1" && o.Price == 100);
            orders.Should().Contain(o => o.Product == "Product2" && o.Price == 200);
        }
    }
}