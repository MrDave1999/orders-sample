using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new OrderStatusConfiguration())
            .ApplyConfiguration(new CustomerConfiguration())
            .ApplyConfiguration(new OrderConfiguration())
            .ApplyConfiguration(new OrderDetailConfiguration());
    }
}
