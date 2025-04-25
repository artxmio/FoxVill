using FoxVill.DataBase;
using FoxVill.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace FoxVill.MainServices.ProductService;

public class ProductService : IProductService
{
    private readonly DatabaseContext _dbContext;
    private Product _selectedProduct;

    private int _countProducts = 10;

    public int CountProducts
    {
        get => _countProducts;
        set => _countProducts = value;
    }


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

    public ObservableCollection<Product> GetProducts(int currentUserID = 0)
    {
        if (currentUserID == 0)
        {
            return [.. _dbContext.Products.Take(CountProducts)];
        }

        var products = _dbContext.Products
                .Select(p => new Product
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    IsFavorite = _dbContext.Favorites
                        .Any(f => f.UserId == currentUserID && f.ProductId == p.Id)
                })
                .Take(CountProducts)
                .ToList();

        return [.. products.Take(CountProducts)];
    }
}
