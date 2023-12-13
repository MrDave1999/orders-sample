using Application.Shared;
using Domain;
using FluentValidation;
using SimpleResults;

namespace Application.Features.Orders;

public class CreateOrderRequest
{
    public int CustomerId { get; init; }
    public string Description { get; init; }
    public string DeliveryAddress { get; init; }
    public DateOnly Date { get; init; }
    public DateOnly ShippedDate { get; init; }
    public DateOnly DeliveryDate { get; init; }
    public IEnumerable<CreateOrderDetailRequest> Details { get; init; }

    public CreateOrderRequest()
    {
        Details = Enumerable.Empty<CreateOrderDetailRequest>();
    }

    public Order MapToOrder() => new()
    {
        CustomerId = CustomerId,
        Description = Description,
        DeliveryAddress = DeliveryAddress,
        ShippedDate = ShippedDate,
        DeliveryDate = DeliveryDate
    };
}

public class CreateOrderDetailRequest
{
    public string Product { get; init; }
    public int Amount { get; init; }
    public double Price { get; init; }

    public OrderDetail MapToOrderDetail() => new()
    {
        Product = Product,
        Amount = Amount,
        Price = Price
    };
}

public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderValidator()
    {
        RuleFor(request => request.CustomerId).NotEmpty().GreaterThan(0);
        RuleFor(request => request.Description).NotEmpty();
        RuleFor(request => request.DeliveryAddress).NotEmpty();
        RuleFor(request => request.ShippedDate).NotEmpty();
        RuleFor(request => request.DeliveryDate).NotEmpty();
        RuleFor(request => request.Details).NotEmpty();
        RuleForEach(request => request.Details)
            .ChildRules(i =>
            {
                i.RuleFor(request => request.Product).NotEmpty();
                i.RuleFor(request => request.Amount).NotEmpty().GreaterThan(0);
                i.RuleFor(request => request.Price).NotEmpty().GreaterThan(0);
            });
    }
}

public class CreateOrderUseCase(
    IUnitOfWork unitOfWork,
    IOrderRepository orderRepository)
{
    public async Task<Result<CreatedId>> ExecuteAsync(CreateOrderRequest request)
    {
        var result = new CreateOrderValidator().Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var order = request.MapToOrder();
        orderRepository.Add(order);
        foreach (var detail in request.Details)
            order.Details.Add(detail.MapToOrderDetail());

        await unitOfWork.SaveChangesAsync();
        return Result.CreatedResource(order.Id);
    }
}
