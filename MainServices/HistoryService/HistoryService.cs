using FoxVill.DataBase;
using FoxVill.Model;
using SQLitePCL;
using System.Collections.ObjectModel;

namespace FoxVill.MainServices.HistoryService;

public class HistoryService(DatabaseContext context)
{
    private readonly DatabaseContext _context = context;

    private ObservableCollection<HistoryItem> _historyItems = [.. context.HistoryItems.ToList()];

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