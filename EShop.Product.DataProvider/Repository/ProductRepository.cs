using EShop.Infrastructure.Command.Product;
using EShop.Infrastructure.Event.Product;
using MongoDB.Driver;
using MongoDB.Driver.Linq;


namespace EShop.Product.DataProvider.Repository
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
            product = await _collection.AsQueryable().FirstOrDefaultAsync(x => x.ProductId == ProductId);
            return new ProductCreated() { ProductId = product.ProductId, ProductName = product.ProductName };

        }






    }
}
