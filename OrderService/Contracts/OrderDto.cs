namespace OrderService.Contracts;

public class OrderDto
{
    public string Product { get; set; } = null!;
    public decimal Price { get; set; }
}