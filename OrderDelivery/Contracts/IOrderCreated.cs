namespace OrderDelivery.Contracts;

public interface IOrderCreated
{
    Guid Id { get; }
    string Product { get; }
    decimal Price { get; }
}