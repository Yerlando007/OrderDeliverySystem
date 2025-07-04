using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Db;

public class DeliveryDbContext : DbContext
{
    public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options) : base(options) { }

    public DbSet<Delivery> Deliveries => Set<Delivery>();
}