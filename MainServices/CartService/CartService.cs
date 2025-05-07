using FoxVill.DataBase;
using FoxVill.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace FoxVill.MainServices.CartService;

public class CartService
{
    private readonly DatabaseContext _context;

    public CartService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task AddItemToBasket(int userId, int itemId, int quantity = 1)
    {
        var user = _context.Users.Include(u => u.Cart).ThenInclude(b => b.CartItems).FirstOrDefault(u => u.Id == userId);

        if (user != null && user.Cart != null)
        {
            var cart = user.Cart;
            var item = _context.Products.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                var existingCartItem = cart.CartItems
                    .FirstOrDefault(ci => ci.ItemId == itemId);

                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += quantity;
                }
                else
                {
                    var cartItem = new Model.CartItem
                    {
                        CartId = cart.CartId,
                        ItemId = item.Id,
                        Product = item,
                        Quantity = quantity
                    };
                    cart.CartItems.Add(cartItem);
                }

                await _context.SaveChangesAsync();
            }
        }
    }

    public async Task DeleteItemFromBasket(object _)
    {
        if (_ != null && _ is CartItem itemToRemove)
        {
            var cartItem = _context.CartItems
                .FirstOrDefault(ci => ci.CartItemId == itemToRemove.CartItemId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
