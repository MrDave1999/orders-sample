using Application.Shared;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderStatusRepository(AppDbContext context) : IOrderStatusRepository
{
    public async Task<bool> IsInvalidAsync(int statusId)
        => !await context
            .Set<OrderStatus>()
            .Where(o => o.Id == statusId)
            .AnyAsync();
}
