using System.ComponentModel.DataAnnotations;

namespace FoxVill.Model;

public class OrderItem
{
    [Key]
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }

    public int ItemId { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; } = 1;
    public decimal Price { get; set; }
}
