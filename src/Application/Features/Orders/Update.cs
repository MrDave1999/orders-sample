using Application.Shared;
using Application.Shared.Resources;
using FluentValidation;
using SimpleResults;

namespace Application.Features.Orders;

public class UpdateOrderRequest
{
    public int OrderStatusId { get; init; }
}

public class UpdateOrderValidator : AbstractValidator<UpdateOrderRequest>
{
    public UpdateOrderValidator()
    {
        RuleFor(request => request.OrderStatusId)
            .NotEmpty()
            .GreaterThan(0);
    }
}

public class UpdateOrderUseCase(
    IUnitOfWork unitOfWork, 
    IOrderRepository orderRepository,
    IOrderStatusRepository orderStatusRepository)
{
    public async Task<Result> ExecuteAsync(int id, UpdateOrderRequest request)
    {
        var result = new UpdateOrderValidator().Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var order = await orderRepository.GetByIdAsync(id);
        if (order is null)
            return Result.NotFound();

        if (await orderStatusRepository.IsInvalidAsync(request.OrderStatusId))
            return Result.Invalid(Messages.InvalidOrderStatus);

        order.OrderStatusId = request.OrderStatusId;
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
