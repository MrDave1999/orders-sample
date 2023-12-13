namespace Domain;

public class Order
{
    public const int Pending    = 1;
    public const int Confirmed  = 2;
    public const int InProgress = 3;
    public const int Delivered  = 4;
    public const int Cancelled  = 5;

    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public string Description { get; set; }
    public string DeliveryAddress { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly ShippedDate { get; set; }
    public DateOnly DeliveryDate { get; set; }
    public int OrderStatusId { get; set; } = Pending;
    public OrderStatus OrderStatus { get; set; }

    public List<OrderDetail> Details { get; set; } = new();
    public double CalculateTotal()
    {
        double total = 0;
        foreach (OrderDetail detail in Details)
            total += detail.SubTotal;

        return total;
    }

    public void Cancel() => OrderStatusId = Cancelled;
    public bool IsCanceled() => OrderStatusId == Cancelled;
}
