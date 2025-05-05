using FoxVill.Command;
using FoxVill.DataBase;
using FoxVill.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace FoxVill.ViewModel;
 
public class AddNewPaymentMethodViewModel : INotifyPropertyChanged
{
    private readonly DatabaseContext _dbContext;
    private readonly User _currentUser;

    public ICommand AddNewMethodCommand { get; }
    public PaymentMethod NewPaymentMethod { get; } = new();

    private DateTime _date;

    public event PropertyChangedEventHandler? PropertyChanged;

    public DateTime Date
    {
        get => _date; 
        set
        {
            _date = value;
            OnPropertyChanged();
        }
    }

    public AddNewPaymentMethodViewModel(DatabaseContext context, User currentUser)
    {
        this._dbContext = context;
        this._currentUser = currentUser;

        _date = DateTime.Now;

        AddNewMethodCommand = new RelayCommand(async p => await Add(p));
    }

    private async Task Add(object p)
    {
        if (string.IsNullOrWhiteSpace(NewPaymentMethod.CardNumber))
        {
            MessageBox.Show("Введите номер карты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (!Regex.IsMatch(NewPaymentMethod.CardNumber, @"^\d{16}$"))
        {
            return;
        }

        if (Date < DateTime.Today)
        {
            return;
        }

        var exitingMethod = await _dbContext.PaymentMethods.FirstOrDefaultAsync(p => p.Id == _currentUser.Id && p.CardNumber == NewPaymentMethod.CardNumber);

        if (exitingMethod is null)
        {
            NewPaymentMethod.UserId = _currentUser.Id;
            NewPaymentMethod.ExpiryDate = Date.ToShortDateString();
            await _dbContext.PaymentMethods.AddAsync(NewPaymentMethod);
            await _dbContext.SaveChangesAsync();

            ((Window)p).Close();
        }
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
