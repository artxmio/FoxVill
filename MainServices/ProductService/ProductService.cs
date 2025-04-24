using FoxVill.DataBase;
using FoxVill.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace FoxVill.MainServices.ProductService;

public class ProductService : IProductService
{
    private readonly DatabaseContext _dbContext;
    private Product _selectedProduct;

    public Product SelectedProduct
    {
        get => _selectedProduct;
        set => _selectedProduct = value;
    }

    public ProductService(DatabaseContext context)
    {
        _dbContext = context;
        _selectedProduct = new Product();
    }

    public ObservableCollection<Product> GetProducts(int countProduct = 10, int currentUserID = 0)
    {
        _dbContext.Products.Load();

        var products = _dbContext.Products.ToList();

        var favoriteProductIds = _dbContext.Favorites
            .Where(f => f.UserId == currentUserID)
            .Select(f => f.ProductId)
            .ToHashSet();

        foreach (var product in products)
        {
            product.IsFavorite = favoriteProductIds.Contains(product.Id);
        }

        return [.. products.Take(countProduct)];
    }
}
