using FoxVill.DataBase;
using FoxVill.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace FoxVill.MainServices.FavoritesService;

public class FavoriteService : IFavoriteService
{
    private readonly DatabaseContext _databaseContext;
    private readonly User _currentUser;
    private ObservableCollection<Product> _product = null!;

    public ObservableCollection<Product> Products
    {
        get => _product; 
        set => _product = value;
    }


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
}
