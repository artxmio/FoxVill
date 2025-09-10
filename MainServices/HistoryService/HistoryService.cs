using FoxVill.DataBase;
using FoxVill.Model;
using System.Collections.ObjectModel;

namespace FoxVill.MainServices.HistoryService;

public class HistoryService(DatabaseContext context, int userID)
{
    private readonly DatabaseContext _context = context;

    private ObservableCollection<HistoryItem> _historyItems = [.. context.HistoryItems.Where(h => h.UserId == userID).ToList()];

    public ObservableCollection<HistoryItem> HistoryItems
    {
        get => _historyItems; 
        set => _historyItems = value;
    }

    public void AddOrderToPurchaseHistory(int userId, List<OrderItem> orderItems)
    {
        foreach (var orderItem in orderItems)
        {
            var purchaseHistory = new HistoryItem
            {
                UserId = userId,
                ProductName = orderItem.Product.Title,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price,
                PurchaseDate = DateTime.Now
            };

            _context.HistoryItems.Add(purchaseHistory);
        }

        _context.SaveChanges();
    }
}