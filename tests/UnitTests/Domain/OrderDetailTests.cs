using Domain;

namespace UnitTests.Domain;

public class OrderDetailTests
{
    [Test]
    public void SubTotal_ShouldReturnsSubtotalOfProduct()
    {
        // Arrange
        double expected = 26800;
        var orderDetail = new OrderDetail
        {
            Price = 13400,
            Amount = 2
        };

        // Act
        double actual = orderDetail.SubTotal;

        // Assert
        actual.Should().Be(expected);
    }
}
