using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Shared.Resources;

namespace Infrastructure.Configurations;

public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        var statuses = new OrderStatus[]
        {
            new() { Id = 1, Name = Messages.Pending },
            new() { Id = 2, Name = Messages.Confirmed },
            new() { Id = 3, Name = Messages.InProgress },
            new() { Id = 4, Name = Messages.Delivered },
            new() { Id = 5, Name = Messages.Cancelled }
        };
        builder.HasData(statuses);
    }
}
