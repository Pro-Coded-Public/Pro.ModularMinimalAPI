using ValidationModule.Models;

namespace ValidationModule.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> CreateProduct(Product product);
}

public class ProductService : IProductService
{
    public async Task<IEnumerable<Product>> GetProducts()
    {
        // return a list of products

        return new List<Product>
        {
            new()
            {
                Id = 1,
                Name = "Product 1",
                Price = 1.99m,
                Category = "Category 1"
            },
            new()
            {
                Id = 2,
                Name = "Product 2",
                Price = 2.99m,
                Category = "Category 2"
            },
            new()
            {
                Id = 3,
                Name = "Product 3",
                Price = 3.99m,
                Category = "Category 3"
            }
        };
    }

    public async Task<Product> CreateProduct(Product product)
    {
        return product;
    }
}