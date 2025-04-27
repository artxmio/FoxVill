namespace FoxVill.Model;

public class PaymentMethod
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string CardNumber { get; set; } = "";
    public string ExpiryDate { get; set; } = "";

    public User User { get; set; }
}
