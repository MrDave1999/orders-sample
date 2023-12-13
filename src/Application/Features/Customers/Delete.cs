using Application.Shared;
using SimpleResults;

namespace Application.Features.Customers;

public class DeleteCustomerUseCase(
    IUnitOfWork unitOfWork,
    ICustomerRepository customerRepository)
{
    public async Task<Result> ExecuteAsync(int id)
    {
        var customer = await customerRepository.GetByIdAsync(id);
        if (customer is null)
            return Result.NotFound();

        customerRepository.Remove(customer);
        await unitOfWork.SaveChangesAsync();
        return Result.DeletedResource();
    }
}
