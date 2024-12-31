using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManager.BLL.DTOs.Product;
using StockManager.BLL.Services.ProductServices;

namespace StockManager.API.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _productService.GetAllProducts();

            if (!response.Status)
                return BadRequest(response.Message);
            return Ok(response.Data);
        }

        [AllowAnonymous]
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById([FromRoute] int productId)
        {
            var response = await _productService.GetProductById(productId);

            if (!response.Status)
                return BadRequest(response.Message);
            return Ok(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] addProductDto addProductDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _productService.CreateProduct(addProductDto);
            if (!response.Status)
                return BadRequest(response.Message);
            return Ok(response.Data);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int productId, [FromBody] editProductDto editProductDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _productService.UpdateProduct(productId, editProductDto);

            if (!response.Status)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int productId)
        {
            var response = await _productService.DeleteProduct(productId);

            if (!response.Status)
                return BadRequest(response.Message);
            return NoContent();
        }
    }
}