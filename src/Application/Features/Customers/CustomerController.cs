using Microsoft.AspNetCore.Mvc;
using SimpleResults;

namespace Application.Features.Customers;

[TranslateResultToActionResult]
[Route("api/customers")]
[ApiController]
public class CustomerController
{
    [HttpPost]
    public async Task<Result<CreatedId>> Create([FromBody]CreateCustomerRequest request, CreateCustomerUseCase useCase)
        => await useCase.ExecuteAsync(request);

    [HttpDelete("{id}")]
    public async Task<Result> Delete(int id, DeleteCustomerUseCase useCase)
        => await useCase.ExecuteAsync(id);

    [HttpPut("{id}")]
    public async Task<Result> Update(int id, [FromBody]UpdateCustomerRequest request, UpdateCustomerUseCase useCase)
        => await useCase.ExecuteAsync(id, request);

    [HttpGet]
    public async Task<ListedResult<GetCustomersResponse>> GetAll(GetCustomersUseCase useCase)
        => await useCase.ExecuteAsync();
}
