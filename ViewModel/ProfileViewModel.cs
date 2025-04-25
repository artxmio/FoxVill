using FoxVill.Command;
using FoxVill.DataBase;
using FoxVill.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FoxVill.ViewModel;

public class ProfileViewModel : INotifyPropertyChanged
{
    private readonly DatabaseContext _dbContext;
    private User _currentUser;

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

    public ICommand ApplyUserDataChanges { get; }

    public ProfileViewModel(DatabaseContext dbContext, User currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;

        ApplyUserDataChanges = new RelayCommand(async p => await UpdateUserData());
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
