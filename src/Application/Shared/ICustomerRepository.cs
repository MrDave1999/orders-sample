using Domain;

namespace Application.Shared;

public interface ICustomerRepository
{
    void Add(Customer customer);
    void Remove(Customer customer);
    Task<Customer> GetByIdAsync(int id);
    Task<IEnumerable<Customer>> GetAllAsync();
}
