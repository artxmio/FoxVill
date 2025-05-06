using FoxVill.DataBase;
using FoxVill.Model;
using System.Collections.ObjectModel;

namespace FoxVill.MainServices.PaymentService;

public class PaymentService
{
    private readonly DatabaseContext _dbContext;

    public PaymentService(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ObservableCollection<PaymentMethod> GetPaymentMethods(int currentUserID = 0)
    {
        if (currentUserID == 0)
        {
            return [];
        }

        var methods = _dbContext.PaymentMethods
            .Where(p => p.UserId == currentUserID)
            .ToList();

        return [.. methods];
    }
}
