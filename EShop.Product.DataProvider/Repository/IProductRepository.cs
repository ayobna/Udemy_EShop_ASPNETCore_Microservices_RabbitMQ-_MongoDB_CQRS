using EShop.Infrastructure.Command.Product;
using EShop.Infrastructure.Event.Product;

namespace EShop.Product.DataProvider.Repository
{
    public interface IProductRepository
    {
        Task<ProductCreated> AddProduct(CreateProduct product);
        Task<ProductCreated> GetProduct(string ProductId);
    }
}
