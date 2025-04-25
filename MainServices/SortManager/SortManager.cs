using FoxVill.Model;
using System.Collections.ObjectModel;

namespace FoxVill.MainServices.SortManager;

public static class SortManager
{
    public static ObservableCollection<Product> SortByPrice(ObservableCollection<Product> items, bool ascending)
    {
        return ascending ? [.. items.OrderBy(i => i.Price)] : [.. items.OrderByDescending(i => i.Price)];
    }

    public static ObservableCollection<Product> SortByTitle(ObservableCollection<Product> items, bool ascending)
    {
        return ascending ? [.. items.OrderBy(i => i.Title)] : [.. items.OrderByDescending(i => i.Title)];
    }
}