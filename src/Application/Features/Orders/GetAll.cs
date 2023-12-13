using Application.Shared;
using SimpleResults;

namespace Application.Features.Orders;

public class GetOrdersResponse
{
    public class Detail
    {
        public string Product { get; init; }
        public int Amount { get; init; }
        public double Price { get; init; }
        public double SubTotal { get; init; }
    }

    public string Customer { get; init; }
    public string Description { get; init; }
    public string DeliveryAddress { get; init; }
    public DateOnly Date { get; init; }
    public DateOnly ShippedDate { get; init; }
    public DateOnly DeliveryDate { get; init; }
    public string Status { get; init; }
    public double Total { get; init; }
    public IEnumerable<Detail> Details { get; init; }
}

public class GetOrdersUseCase(IOrderRepository orderRepository)
{
    public async Task<ListedResult<GetOrdersResponse>> ExecuteAsync()
    {
        var orders = await orderRepository.GetAllAsync();
        var response = orders.Select(o => new GetOrdersResponse
        {
            Customer = o.Customer.FullName,
            Description = o.Description,
            DeliveryAddress = o.DeliveryAddress,
            Date = o.Date,
            ShippedDate = o.ShippedDate,
            DeliveryDate = o.DeliveryDate,
            Status = o.OrderStatus.Name,
            Details = o.Details.Select(d => new GetOrdersResponse.Detail
            {
                Product = d.Product,
                Amount = d.Amount,
                Price = d.Price,
                SubTotal = d.SubTotal
            }),
            Total = o.CalculateTotal()
        });
        return Result.ObtainedResources(response);
    }
}
