using FoxVill.Command;
using FoxVill.DataBase;
using FoxVill.MainServices.PaymentService;
using FoxVill.Model;
using FoxVill.View;
using Microsoft.EntityFrameworkCore;
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
    private ObservableCollection<PaymentMethod> _paymentMethods;

    private PaymentMethod _newPaymentMethod;

    public PaymentMethod NewPaymentMethod
    {
        get => _newPaymentMethod;
        set
        {
            _newPaymentMethod = value;
            OnPropertyChanged();
        }
    }


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

    public ObservableCollection<PaymentMethod> PaymentMethods
    {
        get => _paymentMethods;
        set
        {
            _paymentMethods = value;
            OnPropertyChanged();
        }
    }

    public ICommand ApplyUserDataChanges { get; }

    public ICommand AddNewPaymentMethodCommand { get; }
    public ICommand RemovePaymentMethodCommand { get; }

    public ProfileViewModel(DatabaseContext dbContext, User currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
        _paymentsService = new PaymentService(_dbContext);


        _paymentMethods = _paymentsService.GetPaymentMethods(_currentUser.Id);
        OnPropertyChanged(nameof(PaymentMethods));

        ApplyUserDataChanges = new RelayCommand(async p => await UpdateUserData());

        AddNewPaymentMethodCommand = new RelayCommand(async p => await AddNewPaymentMethod());
        RemovePaymentMethodCommand = new RelayCommand(async p => await RemovePaymentMethod(p));
    }

    private async Task RemovePaymentMethod(object parametr)
    {
        if (parametr is not PaymentMethod method)
            return;

        _dbContext.PaymentMethods.Remove(method);
        await _dbContext.SaveChangesAsync();
    }

    private async Task AddNewPaymentMethod()
    {
        var viewModel = new AddNewPaymentMethodViewModel(_dbContext, _currentUser);
        var window = new AddNewPaymentMethodWindow(viewModel);

        window.ShowDialog();
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
