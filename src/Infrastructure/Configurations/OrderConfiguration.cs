using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .Property(o => o.Description)
            .HasColumnType("varchar(100)");

        builder
            .Property(o => o.DeliveryAddress)
            .HasColumnType("varchar(60)");

        var order = new Order
        {
            Id = 1,
            CustomerId = 1,
            Description = string.Empty,
            DeliveryAddress = "Calle 38A #80-72, Medellin, Colombia",
            Date = new DateOnly(2023, 12, 12),
            ShippedDate = new DateOnly(2023, 12, 15),
            DeliveryDate = new DateOnly(2023, 12, 20)
        };
        builder.HasData(order);
    }
}
