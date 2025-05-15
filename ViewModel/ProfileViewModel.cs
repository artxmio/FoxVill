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
using System.Runtime.CompilerServices;
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

    public ProfileViewModel(DatabaseContext dbContext, User currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
        _paymentsService = new PaymentService(_dbContext);
        _historyService = new HistoryService(_dbContext);

        HistoryItems = _historyService.HistoryItems;

        _chartService = new ChartService(_dbContext);

        _paymentMethods = _paymentsService.GetPaymentMethods(_currentUser.Id);
        OnPropertyChanged(nameof(PaymentMethods));

        ApplyUserDataChanges = new RelayCommand(async p => await UpdateUserData());

        AddNewPaymentMethodCommand = new RelayCommand(p => AddNewPaymentMethod());
        RemovePaymentMethodCommand = new RelayCommand(async p => await RemovePaymentMethod(p));
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
