namespace Domain;

public class OrderDetail
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public string Product {  get; set; }
    public int Amount { get; set; }
    public double Price { get; set; }
    public double SubTotal => Amount * Price;
}
