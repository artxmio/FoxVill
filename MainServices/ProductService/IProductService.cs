using FoxVill.Model;
using System.Collections.ObjectModel;

namespace FoxVill.MainServices.ProductService;

public interface IProductService
{
    ObservableCollection<Product> GetProducts(int countProduct = 10, int currentUserID = 0);
}