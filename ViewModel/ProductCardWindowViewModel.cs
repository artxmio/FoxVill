using FoxVill.DataBase;
using FoxVill.Model;

namespace FoxVill.ViewModel;

public class ProductCardWindowViewModel
{
    private readonly DatabaseContext _dbContext;

    private Product _selectedProduct;

    public Product SelectedProduct
    {
        get => _selectedProduct;
        set => _selectedProduct = value;
    }

    public ProductCardWindowViewModel(DatabaseContext context, Product selectedProduct)
    {
        this._dbContext = context;
        this._selectedProduct = _dbContext.Products.FirstOrDefault(x => x.Id == selectedProduct.Id) ?? throw new NullReferenceException();
    }
}
