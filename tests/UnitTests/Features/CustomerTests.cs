using Application.Features.Customers;
using Application.Shared;
using Domain;
using SimpleResults;

namespace UnitTests.Features;

public class CustomerTests
{
    [Test]
    public async Task Create_WhenRequestIsInvalid_ShouldReturnsError()
    {
        // Arrange
        var request = new CreateCustomerRequest
        {
            Document = string.Empty,
            Phone = string.Empty
        };
        var useCase = new CreateCustomerUseCase(
            Mock.Create<IUnitOfWork>(), 
            Mock.Create<ICustomerRepository>());

        // Act
        Result<CreatedId> result = await useCase.ExecuteAsync(request);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [Test]
    public async Task Update_WhenRequestIsInvalid_ShouldReturnsError()
    {
        // Arrange
        var request = new UpdateCustomerRequest
        {
            Phone = string.Empty,
            FirstName = string.Empty
        };
        var repository = Mock.Create<ICustomerRepository>();
        var useCase = new UpdateCustomerUseCase(Mock.Create<IUnitOfWork>(), repository);
        Mock.Arrange(() => repository.GetByIdAsync(Arg.AnyInt))
            .ReturnsAsync(new Customer());

        // Act
        Result result = await useCase.ExecuteAsync(1, request);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }
}
