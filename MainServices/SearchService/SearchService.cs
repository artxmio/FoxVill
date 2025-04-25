using FoxVill.Model;
using System.Collections.ObjectModel;

namespace FoxVill.MainServices.SearchService;

public class SearchService
{
    private string _searchString = "";

    public string SearchString
    {
        get => _searchString;
        set => _searchString = value;
    }

    public ObservableCollection<Product> Find(ObservableCollection<Product> collection)
    {
        if (string.IsNullOrEmpty(_searchString))
            return collection;

        return [.. collection.Where(obj => obj.Title.Contains(_searchString, StringComparison.OrdinalIgnoreCase))];
    }
}
