using System.ComponentModel.DataAnnotations;

namespace FoxVill.Model;

public class HistoryItem
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchaseDate { get; set; } = DateTime.Now;
}
