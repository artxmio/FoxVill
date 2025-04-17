using FoxVill.Model.Enums;

namespace FoxVill.Model;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public ProductType Type { get; set; }
}
