using Application.Features.Orders;
using Application.Shared;
using SimpleResults;

namespace UnitTests.Features;

public class OrderTests
{
    [Test]
    public async Task Create_WhenOrderHeaderRequestIsInvalid_ShouldReturnsError()
    {
        // Arrange
        var request = new CreateOrderRequest
        {
            CustomerId = -1,
            Description = string.Empty,
            Details = null
        };
        var useCase = new CreateOrderUseCase(
            Mock.Create<IUnitOfWork>(), 
            Mock.Create<IOrderRepository>());

        // Act
        Result<CreatedId> result = await useCase.ExecuteAsync(request);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [Test]
    public async Task Create_WhenOrderDetailRequestIsInvalid_ShouldReturnsError()
    {
        // Arrange
        var details = new List<CreateOrderDetailRequest>
        {
            new() { Product = string.Empty, Price = 0, Amount = 0 }
        };
        var request = new CreateOrderRequest
        {
            CustomerId = 1,
            Description = "Test",
            DeliveryAddress = "Test",
            Date = new DateOnly(2023, 01, 01),
            ShippedDate = new DateOnly(2023, 01, 05),
            DeliveryDate = new DateOnly(2023, 01, 10),
            Details = details
        };
        var useCase = new CreateOrderUseCase(
            Mock.Create<IUnitOfWork>(),
            Mock.Create<IOrderRepository>());

        // Act
        Result<CreatedId> result = await useCase.ExecuteAsync(request);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [Test]
    public async Task Update_WhenRequestIsInvalid_ShouldReturnsError()
    {
        // Arrange
        var request = new UpdateOrderRequest { OrderStatusId = -1 };
        var useCase = new UpdateOrderUseCase(
            Mock.Create<IUnitOfWork>(),
            Mock.Create<IOrderRepository>(),
            Mock.Create<IOrderStatusRepository>());

        // Act
        Result result = await useCase.ExecuteAsync(1, request);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }
}
