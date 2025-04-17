using FoxVill.AutorizaitionServices;
using FoxVill.Command;
using FoxVill.DataBase;
using FoxVill.Model;
using FoxVill.View;
using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace FoxVill.ViewModel;

public class AuthorizationWindowViewModel : INotifyPropertyChanged
{
    private User _authUser = new();
    private readonly User _regUser = new();

    private string _errorRegistrionMessage = string.Empty;
    private string _errorAuthorizationMessage = string.Empty;
    private string _repeatedPassword = string.Empty;
    private bool _authorizationIsRememberMe;

    public string ErrorAuthorizationMessage
    {
        get => _errorAuthorizationMessage;
        set
        {
            _errorAuthorizationMessage = value;
            OnPropertyChanged();
        }
    }
    public string ErrorRegistrionMessage
    {
        get => _errorRegistrionMessage;
        set
        {
            _errorRegistrionMessage = value;
            OnPropertyChanged();
        }
    }

    public string AuthorizationEmail
    {
        get => _authUser.Email;
        set
        {
            _authUser.Email = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ErrorAuthorizationMessage));
        }
    }
    public string AuthorizationPassword
    {
        get => _authUser.Password;
        set
        {
            _authUser.Password = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ErrorAuthorizationMessage));
        }
    }
    public bool AuthorizationIsRememberMe
    {
        get => _authorizationIsRememberMe;
        set
        {
            _authorizationIsRememberMe = value;
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

    public ICommand AuthorizationCommand { get; }
    public ICommand RegistrationCommand { get; }

    public AuthorizationWindowViewModel()
    {
        CheckSavedUserData();

        RegistrationCommand = new RelayCommand(async p => await RegistrateNewUser());
        AuthorizationCommand = new RelayCommand(async p => await AuthorizateUser());
    }

    private async Task RegistrateNewUser()
    {
        if (string.IsNullOrEmpty(_regUser.Email) && string.IsNullOrEmpty(_regUser.Password) && string.IsNullOrEmpty(RepeatedPassword))
        {
            ErrorRegistrionMessage = "Заполните все поля!";
            return;
        }

        if (RegistrationPassword != RepeatedPassword)
        {
            ErrorRegistrionMessage = "Пароли не совпадают!";
            return;
        }

        var isSucces = await RegistrationService.AddUserToDatabase(_regUser);

        if (isSucces)
        {
            new MessageWindow("Регистрация прошла успешна!").ShowDialog();
        }
        else
        {
            new MessageWindow("Такой пользователь уже существует,\nлибо произошла какая-то ошибка!").ShowDialog();
        }
    }

    private async Task AuthorizateUser()
    {
        if (string.IsNullOrEmpty(_authUser.Email) && string.IsNullOrEmpty(_authUser.Password))
        {
            ErrorAuthorizationMessage = "Заполните все поля!";
            return;
        }

        var isSuccess = await AuthorizationService.AutorizateUser(_authUser);

        if (isSuccess)
        {
            if (AuthorizationIsRememberMe)
            {
                SaveUserData(_authUser);
            }

            var dataBaseContext = new DatabaseContext();

            var viewModel = new MainWindowViewModel(dataBaseContext);
            var window = new MainWindow(viewModel);
            window.Show();
        }
        else
        {
            ErrorAuthorizationMessage = "Такого пользователя не существует.";
        }
    }

    private static void SaveUserData(User user)
    {
        try
        {
            var applicationFolder = AppDomain.CurrentDomain.BaseDirectory;

            var json = JsonConvert.SerializeObject(user);

            File.WriteAllText(applicationFolder + @"\data.json", json);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Произошла неожиданная ошибка: " + ex.Message, "Внимание");
        }
    }

    private void CheckSavedUserData()
    {
        try
        {
            var applicationFolder = AppDomain.CurrentDomain.BaseDirectory;

            var jsonString = File.ReadAllText(applicationFolder + @"\data.json");

            if (jsonString is not null)
            {
                User savedUser = JsonConvert.DeserializeObject<User>(jsonString) 
                    ?? throw new NullReferenceException(); 

                _authUser = savedUser;

                
                AuthorizateUser();
                Application.Current.MainWindow.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Произошла неожиданная ошибка: " + ex.Message, "Внимание");
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
