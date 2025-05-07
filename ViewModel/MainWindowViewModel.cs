using FoxVill.Command;
using FoxVill.DataBase;
using FoxVill.MainServices.CartService;
using FoxVill.MainServices.FavoritesService;
using FoxVill.MainServices.ProductService;
using FoxVill.MainServices.SearchService;
using FoxVill.MainServices.SortManager;
using FoxVill.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Windows.Controls;
using System.Windows.Input;

namespace FoxVill.ViewModel;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly DatabaseContext _dbContext;
    private readonly ProductService _productService;
    private readonly FavoriteService _favoriteService;
    private readonly SearchService _searchService;
    private readonly User _currentUser;
    private readonly CartService _cartService;

    private ObservableCollection<Product> _products = new();

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? SearchStringChanged;

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

    public string SearchString
    {
        get => _searchService.SearchString;
        set
        {
            _searchService.SearchString = value;
            OnPropertyChange();
            SearchStringChanged?.Invoke(this, new PropertyChangingEventArgs(nameof(SearchString)));
        }
    }

    public User CurrentUser
    {
        get => _currentUser;
    }

    public Cart Cart { get; set; }
    public ObservableCollection<CartItem> CartItems { get; set; }
    public decimal CartPrice => CartItems.Sum(c => c.Product.Price * c.Quantity);

    public ICommand ChangeProductFavoriteStateCommand { get; set; }
    public ICommand ShowFavoritesCommand { get; set; }
    public ICommand ShowAllProductsCommand { get; set; }
    public ICommand ShowMoreProductsCommand { get; set; }
    public ICommand SortByDescendingCommand { get; }
    public ICommand SortByAscendingCommand { get; }
    public ICommand SortByTitleCommand { get; }
    public ICommand SortByTitleRevertCommand { get; }

    public ICommand IncreaseCommand { get; }
    public ICommand DecreaseCommand { get; }

    public ICommand AddToCartCommand { get; }
    public ICommand RemoveItemFromCartCommand { get; }

    public MainWindowViewModel(DatabaseContext context, User currentUser)
    {
        _dbContext = context;
        _currentUser = _dbContext.Users.First(u => u.Email == currentUser.Email);
        _favoriteService = new FavoriteService(_dbContext, _currentUser);
        _productService = new ProductService(_dbContext);
        _searchService = new SearchService();
        _cartService = new CartService(_dbContext);

        Cart = _dbContext.Carts.FirstOrDefault(c => c.UserId == _currentUser.Id)
            ?? throw new NullReferenceException();

        _dbContext.Products.Load();
        CartItems = [.. _dbContext.CartItems.Where(c => c.CartId == Cart.CartId)];
        OnPropertyChange(nameof(CartItems));

        _products = _productService.GetProducts(currentUserID: _currentUser.Id);

        ChangeProductFavoriteStateCommand = new RelayCommand(p => ChangeFavoriteState(p));
        ShowFavoritesCommand = new RelayCommand(p => ShowFavorites());
        ShowAllProductsCommand = new RelayCommand(p => ShowProducts());
        ShowMoreProductsCommand = new RelayCommand(p => ShowMoreProducts());

        SortByDescendingCommand = new RelayCommand(_ =>
        {
            Products = SortManager.SortByPrice(Products, false);
            OnPropertyChange(nameof(Products));
        });
        SortByAscendingCommand = new RelayCommand(_ =>
        {
            Products = SortManager.SortByPrice(Products, true);
            OnPropertyChange(nameof(Products));
        });
        SortByTitleCommand = new RelayCommand(_ =>
        {
            Products = SortManager.SortByTitle(Products, true);
            OnPropertyChange(nameof(Products));
        });
        SortByTitleRevertCommand = new RelayCommand(_ =>
        {
            Products = SortManager.SortByTitle(Products, false);
            OnPropertyChange(nameof(Products));
        });

        IncreaseCommand = new RelayCommand(p =>
        {
            if (p is CartItem item)
            {
                item.Quantity++;
                Debug.WriteLine($"Новое значение: {item.Quantity}");
                OnPropertyChange(nameof(CartItems));
                OnPropertyChange(nameof(CartPrice));
                _dbContext.SaveChanges();
            }
        }
        );
        DecreaseCommand = new RelayCommand(p =>
        {
            if (p is CartItem item)
            {
                if (item.Quantity != 1)
                    item.Quantity--;
                Debug.WriteLine($"Новое значение: {item.Quantity}");
                OnPropertyChange(nameof(CartItems));
                OnPropertyChange(nameof(CartPrice));
                _dbContext.SaveChanges();
            }
        });

        AddToCartCommand = new RelayCommand(async p => await AddToCart(p));
        RemoveItemFromCartCommand = new RelayCommand(async p => await RemoveItemFromCart(p));

        SearchStringChanged += OnSearchStringChanged;
    }

    private async Task RemoveItemFromCart(object p)
    {
        if (p is CartItem item)
        {
            _dbContext.Remove(item);
            await _dbContext.SaveChangesAsync();
            CartItems = [.. _dbContext.CartItems.Where(c => c.CartId == Cart.CartId)];
            OnPropertyChange(nameof(CartItems));
            OnPropertyChange(nameof(CartPrice));
            Debug.WriteLine($"Товар {item.ItemId} удалился из корзины");
        }
    }

    private async Task AddToCart(object p)
    {
        if (p is Product product)
        {
            await _cartService.AddItemToBasket(_currentUser.Id, product.Id);
            Debug.WriteLine($"Товар {product.Title} добавился в корзину");
            OnPropertyChange(nameof(CartItems));
            OnPropertyChange(nameof(CartPrice));
        }
    }

    private void ChangeFavoriteState(object parametr)
    {
        if (parametr is not Product product)
            return;

        _favoriteService.ChangeFavoriteState(product.Id);

        product.IsFavorite = !product.IsFavorite;
        OnPropertyChange(nameof(product.IsFavorite));
    }
    private void ShowFavorites()
    {
        Products = _favoriteService.GetFavorites();
    }

    private void ShowProducts()
    {
        _productService.CountProducts = 10;
        Products = _productService.GetProducts(_currentUser.Id);
    }

    private void ShowMoreProducts()
    {
        _productService.CountProducts += 5;
        Products = _productService.GetProducts(_currentUser.Id);
    }

    protected void OnSearchStringChanged(object sender, PropertyChangingEventArgs e)
    {
        if (SearchString.Length >= 3)
        {
            Products = _searchService.Find(_productService.GetProducts(_currentUser.Id));
            OnPropertyChange(nameof(Products));
        }
    }
    protected void OnPropertyChange([CallerMemberName] string propName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
