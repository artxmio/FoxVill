using FoxVill.Model.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoxVill.Model;

public class Product : INotifyPropertyChanged
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; } = "default";
    public ProductType Type { get; set; }

    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    [NotMapped]
    private bool _isFavorite;
    [NotMapped]
    public bool IsFavorite
    {
        get => _isFavorite;
        set
        {
            if (_isFavorite != value)
            {
                _isFavorite = value;
                OnPropertyChanged(nameof(IsFavorite));
            }
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
