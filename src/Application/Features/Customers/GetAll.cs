using Application.Shared;
using SimpleResults;

namespace Application.Features.Customers;

public class GetCustomersResponse
{
    public string Document { get; init; }
    public string FullName { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
}

public class GetCustomersUseCase(ICustomerRepository customerRepository)
{
    public async Task<ListedResult<GetCustomersResponse>> ExecuteAsync()
    {
        var customers = await customerRepository.GetAllAsync();
        var response = customers.Select(c => new GetCustomersResponse
        {
            Document = c.Document,
            FullName = c.FullName,
            Email = c.Email,
            Phone = c.Phone
        });

        return Result.ObtainedResources(response);
    }
}
