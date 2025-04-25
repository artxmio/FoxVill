using FoxVill.Model;
using System.Collections.ObjectModel;

namespace FoxVill.MainServices.ProductService;

public interface IProductService
{
    ObservableCollection<Product> GetProducts(int currentUserID = 0);
    public int CountProducts { get; set; }
}