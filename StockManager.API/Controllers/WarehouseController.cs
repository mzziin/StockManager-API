using Microsoft.AspNetCore.Mvc;
using StockManager.BLL.ApiModels.ProductModels;
using StockManager.BLL.Services.WarehouseService;

namespace StockManager.API.Controllers
{
    [Route("api/warehouse")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly WarehouseService _warehouseService;
        public WarehouseController(WarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }


        [HttpGet("{warehouseId}/transactions")]
        public IActionResult GetTransactions(
            [FromRoute] int warehouseId,
            [FromQuery] string transactionType,
            [FromQuery] string startDate,
            [FromQuery] string endDate,
            [FromQuery] int? pageIndex = 1,
            [FromQuery] int? pageSize = 10
            )
        {
            throw new NotImplementedException();
        }

        [HttpGet("{warehouseId}/transactions/{transactionId}")]
        public IActionResult GetTransactionById([FromRoute] int warehouseId, [FromRoute] int transactionId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{warehouseId}/transactions/sell")]
        public IActionResult SellProduct([FromRoute] int warehouseId, [FromBody] SellProductModel sellProductModel)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{warehouseId}/transactions/purchase")]
        public IActionResult PurchaseProduct([FromRoute] int warehouseId, [FromBody] PurchaseProductModel purchaseProductModel)
        {
            throw new NotImplementedException();
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