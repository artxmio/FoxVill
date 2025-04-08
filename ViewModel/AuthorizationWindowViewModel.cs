using FoxVill.AutorizaitionServices;
using FoxVill.Command;
using FoxVill.DataBase;
using FoxVill.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FoxVill.ViewModel;

public class AuthorizationWindowViewModel : INotifyPropertyChanged
{
    private User _authUser = new();
    private User _regUser = new();

    private string _errorRegistrionMessage = string.Empty;
    private string _repeatedPassword = string.Empty;

    public string ErrorRegistrionMessage
    {
        get => _errorRegistrionMessage;
        set
        {
            _errorRegistrionMessage = value;
            OnPropertyChanged();
        }
    }

    public User AuthUser
    {
        get => _authUser;

        set
        {
            _authUser = value;
            OnPropertyChanged();
        }
    }
    public User RegUser
    {
        get => _regUser;
        set
        {
            _regUser = value;
            OnPropertyChanged();
        }
    }

    public string RegistrationEmail
    {
        get => _regUser.Email;
        set
        {
            _regUser.Email = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ErrorRegistrionMessage));
        }
    }
    public string RegistrationPassword
    {
        get => _regUser.Password;
        set
        {
            _regUser.Password = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ErrorRegistrionMessage));
        }
    }
    public string RepeatedPassword
    {
        get => _repeatedPassword;
        set
        {
            _repeatedPassword = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ErrorRegistrionMessage));
        }
    }

    public ICommand RegistrationCommand { get; }

    public AuthorizationWindowViewModel()
    {
        RegistrationCommand = new RelayCommand(async p => await RegistrateNewUser());
    }

    private async Task RegistrateNewUser()
    {
        if (string.IsNullOrEmpty(RegUser.Email) && string.IsNullOrEmpty(RegUser.Password))
        {
            ErrorRegistrionMessage = "Заполните все поля!";
            return;
        }

        if (RegistrationPassword != RepeatedPassword)
        {
            ErrorRegistrionMessage = "Пароли не совпадают!";
            return;
        }

        await RegistrationService.AddUserToDatabase(RegUser);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
