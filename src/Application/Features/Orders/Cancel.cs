using Application.Shared;
using Application.Shared.Resources;
using SimpleResults;

namespace Application.Features.Orders;

public class CancelOrderUseCase(
    IUnitOfWork unitOfWork, 
    IOrderRepository orderRepository)
{
    public async Task<Result> ExecuteAsync(int id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        if (order is null)
            return Result.NotFound();

        if (order.IsCanceled())
            return Result.Conflict(Messages.OrderIsAlreadyCanceled);

        order.Cancel();
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
