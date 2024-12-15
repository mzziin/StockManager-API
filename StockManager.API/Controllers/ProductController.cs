using Microsoft.AspNetCore.Mvc;
using StockManager.BLL.ApiModels.ProductModels;

namespace StockManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById([FromRoute] int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] CreateProductModel createProductModel)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct([FromBody] UpdateProductModel updateProductModel)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            throw new NotImplementedException();
        }
    }
}