using FoxVill.DataBase;
using FoxVill.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace FoxVill.MainServices.FavoritesService;

public class FavoriteService : IFavoriteService
{
    private readonly DatabaseContext _databaseContext;
    private readonly User _currentUser;

    public FavoriteService(DatabaseContext context, User currentUser)
    {
        _databaseContext = context;

        _currentUser = currentUser;
    }

    public async void ChangeFavoriteState(int productID)
    {
        var exitingFavorite = await _databaseContext.Favorites.FirstOrDefaultAsync(f => f.UserId == _currentUser.Id && f.ProductId == productID);

        if (exitingFavorite is null)
        {
            var favorite = new Favorite()
            {
                UserId = _currentUser.Id,
                ProductId = productID
            };

            await _databaseContext.Favorites.AddAsync(favorite);
        }
        else
        {
            _databaseContext.Remove(exitingFavorite);
        }

        await _databaseContext.SaveChangesAsync();
    }

    public ObservableCollection<Product> GetFavorites()
    {
        var favoriteProductIds = _databaseContext.Favorites
            .Where(f => f.UserId == _currentUser.Id)
            .Select(f => f.ProductId)
            .ToHashSet();

        var products = _databaseContext.Products
                .Select(p => new Product
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    IsFavorite = _databaseContext.Favorites
                        .Any(f => f.UserId == _currentUser.Id && f.ProductId == p.Id)
                })
                .Where(p => p.IsFavorite)
                .ToList();

        return [.. products];
    }
}
