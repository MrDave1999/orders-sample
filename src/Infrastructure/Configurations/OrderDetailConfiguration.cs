using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        var orderDetails = new OrderDetail[]
        {
            new()
            {
                Id = 1,
                OrderId = 1,
                Product = "Clean Code",
                Amount = 2,
                Price = 187935
            },
            new()
            {
                Id = 2,
                OrderId = 1,
                Product = "A las Puertas del Abismo",
                Amount = 2,
                Price = 2580
            },
            new()
            {
                Id = 3,
                OrderId = 1,
                Product = "Los Besos Robados de Bridget",
                Amount = 3,
                Price = 10580
            }
        };
        builder.HasData(orderDetails);
    }
}
