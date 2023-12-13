using Application.Shared;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomerRepository(AppDbContext context) : ICustomerRepository
{
    public void Add(Customer customer) 
        => context.Add(customer);

    public async Task<IEnumerable<Customer>> GetAllAsync()
        => await context.Set<Customer>().ToListAsync();

    public async Task<Customer> GetByIdAsync(int id)
        => await context
            .Set<Customer>()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();

    public void Remove(Customer customer)
        => context.Remove(customer);
}
