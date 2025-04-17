using FoxVill.DataBase;
using FoxVill.MainServices.ProductService;
using FoxVill.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace FoxVill.ViewModel;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly DatabaseContext _dbContext;
    private readonly ProductService _productService;

    private ObservableCollection<Product> _products;

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<Product> Products
    {
        get => _products;
        set
        {
            _products = value;
            OnPropertyChange(nameof(Products));
        }
    }

    public MainWindowViewModel(DatabaseContext context)
    {
        _dbContext = context;
        _productService = new ProductService(_dbContext);

        _products = _productService.GetProducts();
        OnPropertyChange(nameof(Products));
    }

    protected void OnPropertyChange([CallerMemberName] string propName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
