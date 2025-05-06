using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoxVill.Model;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";

    public Cart Cart { get; set; }

    [JsonIgnore]
    public ICollection<Favorite> Favorites { get; set; } = [];
    [JsonIgnore]
    public ICollection<PaymentMethod> PaymentMethods { get; set; } = []; 
}
