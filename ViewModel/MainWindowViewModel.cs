using FoxVill.Command;
using FoxVill.DataBase;
using FoxVill.MainServices.FavoritesService;
using FoxVill.MainServices.ProductService;
using FoxVill.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FoxVill.ViewModel;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly DatabaseContext _dbContext;
    private readonly ProductService _productService;
    private readonly FavoriteService _favoriteService;
    private readonly User _currentUser;

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
    public Product SelectedProduct
    {
        get => _productService.SelectedProduct;
        set
        {
            _productService.SelectedProduct = value;
            OnPropertyChange(nameof(SelectedProduct));
        }
    }

    public ICommand ChangeProductFavoriteStateCommand {  get; set; }

    public MainWindowViewModel(DatabaseContext context, User currentUser)
    {
        _dbContext = context;
        _currentUser = _dbContext.Users.First(u => u.Email == currentUser.Email);
        _favoriteService = new FavoriteService(_dbContext, _currentUser);
        _productService = new ProductService(_dbContext);

        _products = _productService.GetProducts(currentUserID: _currentUser.Id);

        ChangeProductFavoriteStateCommand = new RelayCommand(p => ChangeFavoriteState(p));
    }

    private void ChangeFavoriteState(object parametr)
    {
        if (parametr is not Product product)
            return;

        _favoriteService.ChangeFavoriteState(product.Id);

        product.IsFavorite = !product.IsFavorite;
        OnPropertyChange(nameof(product.IsFavorite));
    }

    protected void OnPropertyChange([CallerMemberName] string propName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
