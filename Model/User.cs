using System.ComponentModel.DataAnnotations;

namespace FoxVill.Model;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";

    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
}
