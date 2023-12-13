using Application.Shared;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository(AppDbContext context) : IOrderRepository
{
    public void Add(Order order)
        => context.Add(order);

    public async Task<IEnumerable<Order>> GetAllAsync()
        => await context
            .Set<Order>()
            .Include(o => o.Customer)
            .Include(o => o.Details)
            .Include(o => o.OrderStatus)
            .ToListAsync();

    public async Task<Order> GetByIdAsync(int id)
        => await context
            .Set<Order>()
            .Where(o => o.Id == id)
            .FirstOrDefaultAsync();
}
