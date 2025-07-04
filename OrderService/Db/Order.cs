namespace OrderService.Db;

public class Order
{
    public Guid Id { get; set; }
    public string Product { get; set; } = null!;
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}