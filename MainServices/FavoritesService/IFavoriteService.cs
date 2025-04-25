using FoxVill.Model;
using System.Collections.ObjectModel;

namespace FoxVill.MainServices.FavoritesService;

public interface IFavoriteService
{
    ObservableCollection<Product> GetFavorites();
    void ChangeFavoriteState(int productID);
}
