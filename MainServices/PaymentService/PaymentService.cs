using FoxVill.DataBase;
using FoxVill.Model;
using System.Collections.ObjectModel;

namespace FoxVill.MainServices.PaymentService;

public class PaymentService
{
    private readonly EncryptService _encryptService;
    private readonly DatabaseContext _dbContext;

    public PaymentService(DatabaseContext dbContext)
    {
        _encryptService = new EncryptService();
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

        methods.Add(new PaymentMethod()
        {
            Id = 1,
            CardNumber = "23123123123",
            ExpiryDate = "12.23.2123"
        });

        return [.. methods];
    }
}
