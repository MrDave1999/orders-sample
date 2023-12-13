using Domain;

namespace UnitTests.Domain;

public class OrderTests
{
    [Test]
    public void CalculateTotal_ShouldReturnsTotalValueToBePaid()
    {
        // Arrange
        double expectedTotal = 26000;
        var order = new Order
        {
            Details = new List<OrderDetail> 
            {
                new() { Amount = 1, Price = 3000 },
                new() { Amount = 2, Price = 4000 },
                new() { Amount = 3, Price = 5000 }
            }
        };

        // Act
        double actual = order.CalculateTotal();

        // Assert
        actual.Should().Be(expectedTotal);
    }

    [Test]
    public void IsCanceled_WhenStatusIsCanceled_ShouldReturnsTrue()
    {
        // Arrange
        var order = new Order();
        order.Cancel();

        // Act
        bool actual = order.IsCanceled();

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void IsCanceled_WhenStatusIsNotCanceled_ShouldReturnsFalse()
    {
        // Arrange
        var order = new Order();

        // Act
        bool actual = order.IsCanceled();

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void OrderStatusId_WhenOrderIsCreated_ShouldReturnsPendingStatus()
    {
        // Arrange
        var order = new Order();
        int expected = Order.Pending;

        // Act
        int actual = order.OrderStatusId;

        // Assert
        actual.Should().Be(expected);
    }
}
