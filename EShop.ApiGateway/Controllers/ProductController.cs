using EShop.Infrastructure.Command.Product;
using EShop.Infrastructure.Event.Product;
using EShop.Infrastructure.Query.Product;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace EShop.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {



        private IBusControl _bus;
        IRequestClient<GetProductById> _requestClient;
        public ProductController(IBusControl bus ,IRequestClient<GetProductById> request)
        {
            _bus = bus;
            _requestClient = request;
        }
        [HttpGet]
        public async Task<IActionResult> Get(string ProductId)
        {
            var prdct= new GetProductById() { ProductId=ProductId};
            var product = await _requestClient.GetResponse <ProductCreated>(prdct);
            return Accepted(product);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CreateProduct product)
        {
            var uri = new Uri("rabbitmq://localhost/create_product");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(product);

            return Accepted("Product Created");
        }
     
    }
}
