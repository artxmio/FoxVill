using FoxVill.Command;
using FoxVill.DataBase;
using FoxVill.MainServices.CartService;
using FoxVill.MainServices.FavoritesService;
using FoxVill.MainServices.HistoryService;
using FoxVill.MainServices.ProductService;
using FoxVill.MainServices.ReceiptGenerator;
using FoxVill.MainServices.SearchService;
using FoxVill.MainServices.SortManager;
using FoxVill.Model;
using FoxVill.View;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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
    private bool _isOnlinePaymentSelected = false;
    private bool _isOfflinePaymentSelected = false;

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

    public bool IsOnlinePayMentSelected
    {
        get => _isOnlinePaymentSelected;
        set
        {
            _isOnlinePaymentSelected = value;
            OnPropertyChange();
        }
    }

    public bool IsOfflinePayMentSelected
    {
        get => _isOfflinePaymentSelected;
        set
        {
            _isOfflinePaymentSelected = value;
            OnPropertyChange();
        }
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

    public ICommand MakeOrderCommand { get; }
    public ICommand OpenProductCardCommand { get; }

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

        MakeOrderCommand = new RelayCommand(p => MakeOrder());
        OpenProductCardCommand = new RelayCommand(p => OpenProductCard(p));

        SearchStringChanged += OnSearchStringChanged;
    }

    private void OpenProductCard(object product)
    {
        if (product is Product selectedProduct)
        {
            var viewModel = new ProductCardWindowViewModel(_dbContext, selectedProduct);
            var window = new ProductCardWindow(viewModel, this);

            window.ShowDialog();
        }
    }

    private void MakeOrder()
    {
        if (CartItems.Count == 0)
        {
            (new MessageWindow("Ваша корзина пуста!")).Show();
            return;
        }
        if (!IsOfflinePayMentSelected && !IsOnlinePayMentSelected)
        {
            (new MessageWindow("Выберите способ оплаты!")).Show();
            return;
        }

        if (IsOfflinePayMentSelected)
        {
            (new MessageWindow("Покажите чек из сообщения на кассе и произведите оплату!")).Show();
        }
        else
        {
            var viewModel = new OrderWindowViewModel(_dbContext, CurrentUser);
            var window = new OrderWindow(viewModel);

            var a = window.ShowDialog();

            if ((bool)a)
            {
                (new MessageWindow("Оплата прошла успешно!")).Show();
            }
            else
            {
                return;
            }
        }

        var order = new Order()
        {
            UserId = CurrentUser.Id,
            OrderDate = DateTime.Now,
            TotalAmount = Cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity),
            OrderItems = [..Cart.CartItems.Select(ci => new OrderItem()
            {
                ItemId = ci.ItemId,
                Quantity = ci.Quantity,
                Price = ci.Product.Price,
            })]
        };

        _dbContext.Add(order);

        ReceiptGenerator.GenerateReceipt((int)CartPrice, _currentUser.Email);

        HistoryService historyService = new(_dbContext);
        historyService.AddOrderToPurchaseHistory(_currentUser.Id, [.. order.OrderItems]);

        CartItems = [];
        _dbContext.CartItems.RemoveRange(_dbContext.CartItems.Where(ci => ci.CartId == Cart.CartId));
        OnPropertyChange(nameof(CartItems));
        OnPropertyChange(nameof(CartPrice));
        _dbContext.SaveChanges();
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
            CartItems = [.. _dbContext.CartItems.Where(c => c.CartId == Cart.CartId)];
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
