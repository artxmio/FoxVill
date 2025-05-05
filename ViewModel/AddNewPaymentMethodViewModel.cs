using FoxVill.Command;
using FoxVill.DataBase;
using FoxVill.Model;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;

namespace FoxVill.ViewModel;

public class AddNewPaymentMethodViewModel
{
    private readonly DatabaseContext _dbContext;
    private readonly User _currentUser;

    public ICommand AddNewMethodCommand { get; }
    public PaymentMethod NewPaymentMethod { get; } = null!;

    public AddNewPaymentMethodViewModel(DatabaseContext context, User currentUser)
    {
        this._dbContext = context;
        this._currentUser = currentUser;

        AddNewMethodCommand = new RelayCommand(async p => await Add(p));
    }

    private async Task Add(object p)
    {
        var exitingMethod = await _dbContext.PaymentMethods.FirstOrDefaultAsync(p => p.Id == _currentUser.Id && p.CardNumber == NewPaymentMethod.CardNumber);

        if (exitingMethod is null)
        {
            _dbContext.PaymentMethods.Add(NewPaymentMethod);
        }
    }
}
