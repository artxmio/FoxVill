using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoxVill.Model;

public class CartItem : INotifyPropertyChanged
{
    [Key]
    public int CartItemId { get; set; }
    public int CartId { get; set; }
    public Cart Cart { get; set; }

    public int ItemId { get; set; }
    public Product Product { get; set; }

    private int _quantity;
    public int Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;
            OnPropertyChanged(nameof(Quantity));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
