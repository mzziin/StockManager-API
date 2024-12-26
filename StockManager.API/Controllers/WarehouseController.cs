using Microsoft.AspNetCore.Mvc;
using StockManager.BLL.Services.ProductServices;
using StockManager.BLL.Services.WarehouseServices;

namespace StockManager.API.Controllers
{
    [Route("api/warehouse")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;
        private readonly IProductService _productService;

        public WarehouseController(IWarehouseService warehouseService, IProductService productService)
        {
            _warehouseService = warehouseService;
            _productService = productService;
        }


        [HttpGet("{warehouseId}/transactions")]
        public async Task<IActionResult> GetTransactions(
            [FromRoute] int warehouseId,
            [FromQuery] string transactionType,
            [FromQuery] string startDate,
            [FromQuery] string endDate,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10
            )
        {
            var response = await _warehouseService.SearchTransactions(warehouseId, transactionType, startDate, endDate, pageIndex, pageSize);
            if (response.Status)
                return Ok(response.Data);

            return NotFound(new { status = response.Status, message = response.Message });
        }

        [HttpGet("{warehouseId}/transactions/{transactionId}")]
        public async Task<IActionResult> GetTransactionById([FromRoute] int warehouseId, [FromRoute] Guid transactionId)
        {
            var response = await _warehouseService.GetTransactionById(warehouseId, transactionId);

            if (response.Status)
                return Ok(response.Data);

            return NotFound(new { status = response.Status, message = response.Message });
        }

        [HttpPost("{warehouseId}/transactions/sell")]
        public async Task<IActionResult> SellProduct([FromRoute] int warehouseId, [FromQuery] int productId, [FromQuery] int quantity, [FromQuery] Guid customerId)
        {
            var result = await _productService.SellProduct(warehouseId, productId, quantity, customerId);

            if (result.Status)
                return Ok(new { status = result.Status, message = result.Message });

            return BadRequest(new { status = result.Status, message = result.Message });

        }

        [HttpPost("{warehouseId}/transactions/purchase")]
        public async Task<IActionResult> PurchaseProduct([FromRoute] int warehouseId, [FromQuery] int productId, [FromQuery] int quantity, [FromQuery] Guid supplierId)
        {
            var result = await _productService.PurchaseProduct(warehouseId, productId, quantity, supplierId);

            if (result.Status)
                return Ok(new { status = result.Status, message = result.Message });

            return BadRequest(new { status = result.Status, message = result.Message });
        }

        [HttpGet("{warehouseId}/products")]
        public async Task<IActionResult> GetProducts(
            [FromRoute] int warehouseId,
            [FromQuery] int? subcategoryId,
            [FromQuery] string productName,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 20
            )
        {
            var result = await _warehouseService.GetAllProductsFromWarehouse(warehouseId, subcategoryId, productName, pageIndex, pageSize);
            if (result.Status)
                return Ok(result.Data);

            return NotFound(new { status = result.Status, message = result.Message });
        }
    }
}