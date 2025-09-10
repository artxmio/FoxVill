using FoxVill.DataBase;
using FoxVill.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FoxVill.ViewModel;

public class OrderWindowViewModel : INotifyPropertyChanged
{
    private readonly DatabaseContext _dbContext;
    private readonly User _currentUser;
    private ObservableCollection<PaymentMethod> _paymentMethods = [];

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<PaymentMethod> PaymentMethods
    {
        get => _paymentMethods;
        set
        {
            _paymentMethods = value;
            OnPropertyChanged();
        }
    }

    public OrderWindowViewModel(DatabaseContext context, User currentUser)
    {
        this._dbContext = context;

        _currentUser = currentUser;

        PaymentMethods = [.. _dbContext.PaymentMethods.Where(x => x.UserId == _currentUser.Id)]; 
    }

    protected void OnPropertyChanged([CallerMemberName] string propName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}