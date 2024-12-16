using Microsoft.AspNetCore.Mvc;
using StockManager.BLL.ApiModels.ProductModels;

namespace StockManager.API.Controllers
{
    [Route("api/warehouse")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {

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
        public IActionResult GetProducts(
            [FromRoute] int warehouseId,
            [FromQuery] int? categoryId,
            [FromQuery] int? subcategoryId,
            [FromQuery] string productName,
            [FromQuery] string sortBy = "ProductName",
            [FromQuery] string sortOrder = "asc",
            [FromQuery] int? pageIndex = 1,
            [FromQuery] int? pageSize = 20
            )
        {
            throw new NotImplementedException();
        }
    }
}