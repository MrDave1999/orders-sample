using Application.Features.Customers;
using Domain;
using SimpleResults;
using System.Net;

namespace IntegrationTests.Features;

public class CustomerTests : TestBase
{
    [Test]
    public async Task Post_WhenCustomerIsCreated_ShouldReturnsHttpStatusCodeCreated()
    {
        // Arrange
        const int expectedId = 1;
        var client = ApplicationFactory.CreateClient();
        var requestUri = "/api/customers";
        var request = new CreateCustomerRequest
        {
            Document = "0923611853",
            FirstName = "Test",
            LastName = "Test",
            Phone = "123456789",
            Email = "test@hotmail.com",
        };

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
    public async Task Delete_WhenCustomerDoesNotExist_ShouldReturnsHttpStatusCodeNotFound()
    {
        // Arrange
        var client = ApplicationFactory.CreateClient();
        var requestUri = "/api/customers/1000";

        // Act
        var httpResponse = await client.DeleteAsync(requestUri);

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task Delete_WhenCustomerIsDeleted_ShouldReturnsHttpStatusCodeOk()
    {
        // Arrange
        var client = ApplicationFactory.CreateClient();
        var requestUri = "/api/customers/1";
        await AddAsync(new Customer
        {
            Document = "123456789",
            FirstName = "Test",
            LastName = "Test",
            Phone = "123456789",
            Email = "test@hotmail.com"
        });

        // Act
        var httpResponse = await client.DeleteAsync(requestUri);
        var customer = await FindAsync<Customer>(1);

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        customer.Should().BeNull();
    }

    [Test]
    public async Task Update_WhenCustomerDoesNotExist_ShouldReturnsHttpStatusCodeNotFound()
    {
        // Arrange
        var client = ApplicationFactory.CreateClient();
        var requestUri = "/api/customers/1000";
        var request = new UpdateCustomerRequest
        {
            FirstName = "Test",
            LastName = "Test",
            Email = "Test@hotmail.com",
            Phone = "123456789"
        };

        // Act
        var httpResponse = await client.PutAsJsonAsync(requestUri, request);

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task Update_WhenCustomerIsUpdated_ShouldReturnsHttpStatusCodeOk()
    {
        // Arrange
        var client = ApplicationFactory.CreateClient();
        var requestUri = "/api/customers/1";
        await AddAsync(new Customer
        {
            Document = "123456789",
            FirstName = "Test",
            LastName = "Test",
            Phone = "123456789",
            Email = "test@hotmail.com"
        });
        var request = new CreateCustomerRequest
        {
            FirstName = "Bob",
            LastName = "Test",
            Phone = "123456789",
            Email = "test@hotmail.com"
        };

        // Act
        var httpResponse = await client.PutAsJsonAsync(requestUri, request);
        var customer = await FindAsync<Customer>(1);

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        customer.FirstName.Should().Be(request.FirstName);
    }

    [Test]
    public async Task Get_WhenListOfCustomersIsObtained_ShouldReturnsHttpStatusCodeOk()
    {
        // Arrange
        const int expectedCustomers = 2;
        var client = ApplicationFactory.CreateClient();
        var requestUri = "/api/customers";
        await AddRangeAsync([
            new Customer
            {
                Document = "123456789",
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@hotmail.com",
                Phone = "123456789"
            },
            new Customer
            {
                Document = "123456790",
                FirstName = "Test2",
                LastName = "Test2",
                Email = "Test2@hotmail.com",
                Phone = "123456790"
            }
        ]);

        // Act
        var result = await 
            client
            .GetFromJsonAsync<ListedResult<GetCustomersResponse>>(requestUri);

        // Assert
        result.Data.Should().HaveCount(expectedCustomers);
    }
}
