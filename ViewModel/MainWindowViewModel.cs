using FoxVill.DataBase;
using FoxVill.MainServices.ProductService;
using FoxVill.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FoxVill.ViewModel;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly DatabaseContext _dbContext;
    private readonly ProductService _productService;

    private ObservableCollection<Product> _products = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<Product> Products
    {
        get => _products;
        set
        {
            _products = value;
            OnPropertyChange();
        }
    }

    public MainWindowViewModel(DatabaseContext context)
    {
        _dbContext = context;
        _productService = new ProductService(_dbContext);

        _products = _productService.GetProducts();
    }

    protected void OnPropertyChange([CallerMemberName] string propName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
