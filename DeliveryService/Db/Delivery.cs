namespace DeliveryService.Db;

public class Delivery
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string Product { get; set; } = null!;
    public string Status { get; set; } = "Pending";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}