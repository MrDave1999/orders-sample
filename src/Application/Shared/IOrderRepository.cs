using Domain;

namespace Application.Shared;

public interface IOrderRepository
{
    void Add(Order order);
    Task<Order> GetByIdAsync(int id);
    Task<IEnumerable<Order>> GetAllAsync();
}
