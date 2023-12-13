using Application.Shared;
using Domain;
using FluentValidation;
using SimpleResults;

namespace Application.Features.Customers;

public class CreateCustomerRequest
{
    public string Document { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }

    public Customer MapToCustomer() => new()
    {
        Document = Document,
        FirstName = FirstName,
        LastName = LastName,
        Email = Email,
        Phone = Phone
    };
}

public class CreateCustomerValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerValidator()
    {
        RuleFor(request => request.Document).NotEmpty();
        RuleFor(request => request.FirstName).NotEmpty();
        RuleFor(request => request.LastName).NotEmpty();
        RuleFor(request => request.Phone).NotEmpty();
        RuleFor(request => request.Email)
            .NotEmpty()
            .EmailAddress();
    }
}

public class CreateCustomerUseCase(
    IUnitOfWork unitOfWork, 
    ICustomerRepository customerRepository)
{
    public async Task<Result<CreatedId>> ExecuteAsync(CreateCustomerRequest request)
    {
        var result = new CreateCustomerValidator().Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var customer = request.MapToCustomer();
        customerRepository.Add(customer);
        await unitOfWork.SaveChangesAsync();
        return Result.CreatedResource(customer.Id);
    }
}
