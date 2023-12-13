using Application.Shared;
using Domain;
using FluentValidation;
using SimpleResults;

namespace Application.Features.Customers;

public class UpdateCustomerRequest
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }

    public void MapToCustomer(Customer customer)
    {
        customer.FirstName = FirstName;
        customer.LastName = LastName;
        customer.Email = Email;
        customer.Phone = Phone;
    }
}

public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerRequest>
{
    public UpdateCustomerValidator()
    {
        RuleFor(request => request.FirstName).NotEmpty();
        RuleFor(request => request.LastName).NotEmpty();
        RuleFor(request => request.Phone).NotEmpty();
        RuleFor(request => request.Email)
            .NotEmpty()
            .EmailAddress();
    }
}

public class UpdateCustomerUseCase(
    IUnitOfWork unitOfWork, 
    ICustomerRepository customerRepository)
{
    public async Task<Result> ExecuteAsync(int id, UpdateCustomerRequest request)
    {
        var result = new UpdateCustomerValidator().Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var customer = await customerRepository.GetByIdAsync(id);
        if (customer is null)
            return Result.NotFound();

        request.MapToCustomer(customer);
        await unitOfWork.SaveChangesAsync();
        return Result.UpdatedResource();
    }
}
