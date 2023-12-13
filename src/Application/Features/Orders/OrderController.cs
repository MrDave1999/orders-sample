using Microsoft.AspNetCore.Mvc;
using SimpleResults;

namespace Application.Features.Orders;

[TranslateResultToActionResult]
[Route("api/orders")]
[ApiController]
public class OrderController 
{
    [HttpPost]
    public async Task<Result<CreatedId>> Create([FromBody]CreateOrderRequest request, CreateOrderUseCase useCase)
        => await useCase.ExecuteAsync(request);

    [HttpPut("{id}/cancel")]
    public async Task<Result> Cancel(int id, CancelOrderUseCase useCase)
        => await useCase.ExecuteAsync(id);

    [HttpPut("{id}")]
    public async Task<Result> Update(int id, [FromBody]UpdateOrderRequest request, UpdateOrderUseCase useCase)
        => await useCase.ExecuteAsync(id, request);

    [HttpGet]
    public async Task<ListedResult<GetOrdersResponse>> Get(GetOrdersUseCase useCase)
        => await useCase.ExecuteAsync();
}
