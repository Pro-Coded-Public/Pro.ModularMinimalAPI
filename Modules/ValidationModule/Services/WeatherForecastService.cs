using Microsoft.Extensions.Options;
using ValidationModule.Models;

namespace ValidationModule.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetProducts();
    void CreateProduct(Product product);
}

public class ProductService : IProductService
{
    public Task<IEnumerable<Product>> GetProducts()
    {
        throw new NotImplementedException();
    }

    public void CreateProduct(Product product)
    {
        throw new NotImplementedException();
    }
}