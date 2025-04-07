using FoxVill.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FoxVill.ViewModel;

public class AuthorizationWindowViewModel : INotifyPropertyChanged
{
    private User _authUser = new();

    public User AuthUser
    {
        get => _authUser;

        set
        {
            _authUser = value;
            OnPropertyChanged();
        }
    }

    private User _regUser = new();

    public User RegUser
    {
        get => _regUser;
        set
        {
            _regUser = value;
            OnPropertyChanged();
        }
    }

    public string RepeatedPassword { get; set; } = "";

    public AuthorizationWindowViewModel()
    {

    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
