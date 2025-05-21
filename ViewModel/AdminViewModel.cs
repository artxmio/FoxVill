using FoxVill.Command;
using FoxVill.DataBase;
using FoxVill.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace FoxVill.ViewModel;

public class AdminViewModel : INotifyPropertyChanged
{
    private readonly DatabaseContext _dbContext;
    private User _selectedUser = null!;
    private Product _selectedProduct = null!;
    private ObservableCollection<User> _users = null!;
    private ObservableCollection<Product> _products = null!;

    public ObservableCollection<User> Users
    {
        get => _users;
        set { _users = value; OnPropertyChanged(nameof(Users)); }
    }

    public ObservableCollection<Product> Products
    {
        get => _products;
        set { _products = value; OnPropertyChanged(nameof(Products)); }
    }
    public Product SelectedProduct
    {
        get => _selectedProduct;
        set { _selectedProduct = value; OnPropertyChanged(nameof(SelectedProduct)); }
    }
    public User SelectedUser
    {
        get => _selectedUser;
        set { _selectedUser = value; OnPropertyChanged(nameof(SelectedUser)); }
    }
    public ICommand AddUserCommand { get; }
    public ICommand DeleteUserCommand { get; }
    public ICommand AddProductCommand { get; }
    public ICommand DeleteProductCommand { get; }
    public ICommand SaveProductCommand { get; }

    public AdminViewModel(DatabaseContext context)
    {
        _dbContext = context;
        LoadData();

        AddUserCommand = new RelayCommand(p => AddUser());
        DeleteUserCommand = new RelayCommand(p => DeleteUser(p));
        AddProductCommand = new RelayCommand(p =>  AddProduct());
        DeleteProductCommand = new RelayCommand(p =>  DeleteProduct(p));
        SaveProductCommand = new RelayCommand(p => SaveProduct());
    }

    private void SaveProduct()
    {
        _dbContext.SaveChanges();
    }

    private void LoadData()
    {
        Users = [.. _dbContext.Users.ToList()];
        Products = [.. _dbContext.Products.ToList()];
    }

    private void AddUser()
    {
        var newUser = new User { Email = "new@user.com", Password = "1234" };
        _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();
        LoadData();
    }

    private void DeleteUser(object obj)
    {
        if (obj is User user)
        {
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
            LoadData();
        }
    }

    private void AddProduct()
    {
        var newProduct = new Product { Title = "New Product", Price = 0 };
        _dbContext.Products.Add(newProduct);
        _dbContext.SaveChanges();
        LoadData();
    }

    private void DeleteProduct(object obj)
    {
        if (obj is Product product)
        {
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
            LoadData();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged = null!;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}