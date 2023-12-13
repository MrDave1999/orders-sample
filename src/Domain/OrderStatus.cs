namespace Domain;

/// <summary>
/// Status:
/// <list type="bullet">
/// <item>Pending</item>
/// <item>Confirmed</item>
/// <item>In Progress</item>
/// <item>Delivered</item>
/// <item>Cancelled</item>
/// </list>
/// </summary>
public class OrderStatus
{
    public int Id { get; set; }
    public string Name { get; set; }
}
