using Microsoft.AspNetCore.Mvc;
using StockManager.BLL.ApiModels.ProductModels;

namespace StockManager.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        public ProductController()
        {

        }
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById([FromRoute] int productId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] CreateProductModel createProductModel)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct([FromRoute] int productId, [FromBody] UpdateProductModel updateProductModel)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct([FromRoute] int productId)
        {
            throw new NotImplementedException();
        }
    }
}