using EShop.Infrastructure.Command.Product;
using EShop.Infrastructure.Event.Product;
using EShop.Product.Api.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public ProductController(IProductService service)
        {
            _service = service;
        }

        private IProductService _service { get; }

        public async Task<IActionResult> Get(string? productId)
        {
            var product = await _service.GetProduct(productId);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CreateProduct? product )
        {
            product.ProductId = null;
            var addedProduct = await _service.AddProduct(product);
            return Ok(addedProduct);
        }
    }
}
