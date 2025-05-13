using FoxVill.DataBase;
using FoxVill.Model;

namespace FoxVill.MainServices.OrderService;

public class OrderService
{
    private DatabaseContext _dbContext;

    public OrderService(DatabaseContext context)
    {
        _dbContext = context;
    }

    public void AddOrder(Cart cart)
    {
        var order = new Order
        {
            UserId = cart.UserId,
            TotalAmount = cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity),
            OrderItems = [.. cart.CartItems.Select(ci => new OrderItem
            {
                ItemId = ci.ItemId,
                Quantity = ci.Quantity,
                Price = ci.Product.Price
            })]
        };

        _dbContext.Orders.Add(order);
    }
}
