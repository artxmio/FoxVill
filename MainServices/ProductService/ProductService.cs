using FoxVill.DataBase;
using FoxVill.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace FoxVill.MainServices.ProductService;

public class ProductService : IProductService
{
    private readonly DatabaseContext _dbContext;

    public ProductService(DatabaseContext context)
    {
        _dbContext = context;
    }

    public ObservableCollection<Product> GetProducts(int countProduct = 10)
    {
        _dbContext.Products.Load();
        return [.. _dbContext.Products.Take(countProduct)];
    }
}
