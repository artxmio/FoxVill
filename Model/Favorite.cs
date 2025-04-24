using System.ComponentModel.DataAnnotations.Schema;

namespace FoxVill.Model;

public class Favorite
{
    public int FavoriteId { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }

    public User User { get; set; }
    public Product Product { get; set; }
}
