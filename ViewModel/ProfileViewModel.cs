using FoxVill.Command;
using FoxVill.DataBase;
using FoxVill.MainServices.CartService;
using FoxVill.MainServices.ChartService;
using FoxVill.MainServices.HistoryService;
using FoxVill.MainServices.PaymentService;
using FoxVill.Model;
using FoxVill.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Automation.Peers;
using System.Windows.Data;
using System.Windows.Input;

namespace FoxVill.ViewModel;

public class ProfileViewModel : INotifyPropertyChanged
{
    private readonly DatabaseContext _dbContext;
    private readonly User _currentUser;
    private readonly PaymentService _paymentsService;
    private readonly HistoryService _historyService;
    private ObservableCollection<PaymentMethod> _paymentMethods;
    private ChartService _chartService;
    private string _searchText;
    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged(nameof(SearchText));
            HistoryView.Refresh(); // обновляем фильтр
        }
    }

    public ICollectionView HistoryView { get; }
    public event PropertyChangedEventHandler? PropertyChanged;

    public string Email
    {
        get => _currentUser.Email;
        set
        {
            _currentUser.Email = value;
            OnPropertyChanged();
        }
    }
    public string Password
    {
        get => _currentUser.Password;
        set
        {
            _currentUser.Password = value;
            OnPropertyChanged();
        }
    }

    public ChartService ChartService
    {
        get => _chartService;
        private set => _chartService = value;
    }

    public ObservableCollection<PaymentMethod> PaymentMethods
    {
        get => _paymentMethods;
        set
        {
            _paymentMethods = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<HistoryItem> HistoryItems { get; set; }

    public ICommand ApplyUserDataChanges { get; }

    public ICommand AddNewPaymentMethodCommand { get; }
    public ICommand RemovePaymentMethodCommand { get; }

    public ICommand FoxCommand { get; }

    public ProfileViewModel(DatabaseContext dbContext, User currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
        _paymentsService = new PaymentService(_dbContext);
        _historyService = new HistoryService(_dbContext, currentUser.Id);

        HistoryItems = _historyService.HistoryItems;
        HistoryView = CollectionViewSource.GetDefaultView(HistoryItems);
        HistoryView.Filter = FilterHistory;

        _chartService = new ChartService(_dbContext);

        _paymentMethods = _paymentsService.GetPaymentMethods(_currentUser.Id);
        OnPropertyChanged(nameof(PaymentMethods));

        ApplyUserDataChanges = new RelayCommand(async p => await UpdateUserData());

        AddNewPaymentMethodCommand = new RelayCommand(p => AddNewPaymentMethod());
        RemovePaymentMethodCommand = new RelayCommand(async p => await RemovePaymentMethod(p));

        FoxCommand = new RelayCommand(p => OpenGame());
    }

    private bool FilterHistory(object obj)
    {
        if (obj is HistoryItem item)
        {
            if (string.IsNullOrWhiteSpace(SearchText))
                return true;

            return item.ProductName?.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0
                || item.PurchaseDate.ToString("dd.MM.yyyy").Contains(SearchText)
                || item.PurchaseDate.ToString("HH:mm:ss").Contains(SearchText);
        }
        return false;
    }

    private static void OpenGame()
    {
        var window = new CoinWindow();

        if (!CheckPreviousWin())
        {
            window.ShowDialog();
        }
    }

    private static bool CheckPreviousWin()
    {
        if (File.Exists("win_data.txt"))
        {
            string result = File.ReadAllText("win_data.txt");
            return result == "Win";
        }
        return false;
    }

    private async Task RemovePaymentMethod(object parametr)
    {
        if (parametr is not PaymentMethod method)
            return;

        _dbContext.PaymentMethods.Remove(method);
        await _dbContext.SaveChangesAsync();

        _paymentMethods = _paymentsService.GetPaymentMethods(_currentUser.Id);
        OnPropertyChanged(nameof(PaymentMethods));
    }

    private void AddNewPaymentMethod()
    {
        var viewModel = new AddNewPaymentMethodViewModel(_dbContext, _currentUser);
        var window = new AddNewPaymentMethodWindow(viewModel);

        window.ShowDialog();

        _paymentMethods = _paymentsService.GetPaymentMethods(_currentUser.Id);
        OnPropertyChanged(nameof(PaymentMethods));
    }

    private async Task UpdateUserData()
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == _currentUser.Id);

        if (user is not null)
        {
            user.Email = Email;
            user.Password = Password;

            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new NullReferenceException("Пользователь не найден.");
        }
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
