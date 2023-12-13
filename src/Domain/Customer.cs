namespace Domain;

public class Customer
{
    public int Id { get; set; }
    public string Document {  get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => LastName + " " + FirstName;
    public string Email { get; set; }
    public string Phone { get; set; }
}
