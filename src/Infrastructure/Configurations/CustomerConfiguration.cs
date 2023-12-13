using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .HasIndex(c => c.Document)
            .IsUnique();

        var customers = new Customer[]
        {
            new()
            {
                Id = 1,
                Document = "123456789",
                FirstName = "Steven",
                LastName = "Smith",
                Email = "steven_smith@hotmail.com",
                Phone = "3053581035"
            },
            new()
            {
                Id = 2,
                Document = "123456790",
                FirstName = "Dave",
                LastName = "Smith",
                Email = "dave_smith@hotmail.com",
                Phone = "3053581090"
            }
        };
        builder.HasData(customers);
    }
}
