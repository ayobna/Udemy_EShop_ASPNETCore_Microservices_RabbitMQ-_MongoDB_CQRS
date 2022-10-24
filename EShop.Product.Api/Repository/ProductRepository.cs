using EShop.Infrastructure.Command.Product;
using EShop.Infrastructure.Event.Product;
using MongoDB.Driver;

namespace EShop.Product.Api.Repository
{
    public class ProductRepository : IProductRepository
    {

        private IMongoDatabase _database;
        private IMongoCollection<CreateProduct> _collection;
       

        public ProductRepository(IMongoDatabase database)
        {
            _database = database;
            _collection = database.GetCollection<CreateProduct>("Product");
      
        }


        public async Task<ProductCreated> AddProduct(CreateProduct product)
        {
            await _collection.InsertOneAsync(product);
            return new ProductCreated { ProductId = product.ProductId, ProductName = product.ProductName, CreatedAt = DateTime.UtcNow };
        }

        public async Task<ProductCreated> GetProduct(string ProductId)
        {
            var product = new CreateProduct();
            product = await _collection.AsQueryable().FirstOrDefaultAsync();
            return new ProductCreated() { ProductId = product.ProductId, ProductName = product.ProductName };
        }


      

    
    }
}
