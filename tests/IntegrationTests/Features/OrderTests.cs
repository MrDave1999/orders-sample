using Application.Features.Orders;
using Domain;
using SimpleResults;
using System.Net;

namespace IntegrationTests.Features;

public class OrderTests : TestBase
{
    [Test]
    public async Task Post_WhenOrderIsCreated_ShouldReturnsHttpStatusCodeCreated()
    {
        // Arrange
        const int expectedId = 1;
        var client = ApplicationFactory.CreateClient();
        var requestUri = "/api/orders";
        var request = new CreateOrderRequest
        {
            CustomerId = 1,
            Description = "Test",
            DeliveryAddress = "Test",
            Date = new DateOnly(2023, 01, 01),
            ShippedDate = new DateOnly(2023, 01, 05),
            DeliveryDate = new DateOnly(2023, 01, 10),
            Details = [
                new CreateOrderDetailRequest
                {
                    Amount = 1,
                    Price = 1000,
                    Product = "Test"
                }
            ]
        };
        await AddAsync(new Customer { Document = "123456789" });
        this.CreateOrderStatuses();

        // Act
        var httpResponse = await client.PostAsJsonAsync(requestUri, request);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result<CreatedId>>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Data.Id.Should().Be(expectedId);
    }

    [Test]
    public async Task Put_WhenOrderIsCancelled_ShouldReturnsHttpStatusCodeOk()
    {
        // Arrange
        var client = ApplicationFactory.CreateClient();
        var requestUri = "/api/orders/1/cancel";
        this.CreateOrderStatuses();
        await AddAsync(new Customer { Document = "123456789" });
        await AddAsync(new Order { CustomerId = 1, OrderStatusId = Order.Pending });

        // Act
        var httpResponse = await client.PutAsJsonAsync(requestUri, new {});
        var order = await FindAsync<Order>(1);

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        order.OrderStatusId.Should().Be(Order.Cancelled);
    }

    [Test]
    public async Task Put_WhenOrderIsAlreadyCancelled_ShouldReturnsHttpStatusCodeConflict()
    {
        // Arrange
        var client = ApplicationFactory.CreateClient();
        var requestUri = "/api/orders/1/cancel";
        this.CreateOrderStatuses();
        await AddAsync(new Customer { Document = "123456789" });
        await AddAsync(new Order { CustomerId = 1, OrderStatusId = Order.Cancelled });

        // Act
        var httpResponse = await client.PutAsJsonAsync(requestUri, new { });

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Test]
    public async Task Put_WhenOrderStatusIsInvalid_ShouldReturnsHttpStatusCodeBadRequest()
    {
        // Arrange
        var client = ApplicationFactory.CreateClient();
        var requestUri = "/api/orders/1";
        var request = new UpdateOrderRequest { OrderStatusId = 6000 };
        this.CreateOrderStatuses();
        await AddAsync(new Customer { Document = "123456789" });
        await AddAsync(new Order { CustomerId = 1, OrderStatusId = Order.Pending });

        // Act
        var httpResponse = await client.PutAsJsonAsync(requestUri, request);

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task Put_WhenOrderStatusIsChanged_ShouldReturnsHttpStatusCodeOk()
    {
        // Arrange
        var client = ApplicationFactory.CreateClient();
        var requestUri = "/api/orders/1";
        var request = new UpdateOrderRequest { OrderStatusId = Order.Confirmed };
        this.CreateOrderStatuses();
        await AddAsync(new Customer { Document = "123456789" });
        await AddAsync(new Order { CustomerId = 1, OrderStatusId = Order.Pending });

        // Act
        var httpResponse = await client.PutAsJsonAsync(requestUri, request);
        var order = await FindAsync<Order>(1);

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        order.OrderStatusId.Should().Be(request.OrderStatusId);
    }

    [Test]
    public async Task Get_WhenListOfOrdersIsObtained_ShouldReturnsHttpStatusCodeOk()
    {
        // Arrange
        const int expectedOrders = 1;
        const int expectedOrderDetails = 2;
        const int expectedTotal = 56150;
        var client = ApplicationFactory.CreateClient();
        var requestUri = "/api/orders";
        this.CreateOrderStatuses();
        await AddAsync(new Customer { Document = "123456789" });
        await AddAsync(new Order 
        { 
            CustomerId = 1, 
            OrderStatusId = Order.Pending,
            Description = string.Empty,
            DeliveryAddress = "Medellin, Colombia",
            Date = new DateOnly(2023, 01, 01),
            ShippedDate = new DateOnly(2023, 01, 05),
            DeliveryDate = new DateOnly(2023, 01, 10)
        });
        await AddRangeAsync([
            new OrderDetail
            {
                OrderId = 1,
                Amount = 5,
                Price = 5350,
                Product = "Computer"
            },
            new OrderDetail
            {
                OrderId = 1,
                Amount = 4,
                Price = 7350,
                Product = "Laptop"
            }
        ]);

        // Act
        var result = await
            client
            .GetFromJsonAsync<ListedResult<GetOrdersResponse>>(requestUri);

        // Asserts
        result.Data.Should().HaveCount(expectedOrders);
        var results = result.Data.ToList();
        results[0].Details
            .Should()
            .HaveCount(expectedOrderDetails);
        results[0].Total
            .Should()
            .Be(expectedTotal);
    }
}
