using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManager.BLL.Services.AuthServices;
using StockManager.BLL.Services.ProductServices;
using StockManager.BLL.Services.WarehouseServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StockManager.API.Controllers
{
    [Authorize(Roles = "admin, warehouse manager")]
    [Route("api/warehouse")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;
        private readonly IProductService _productService;
        private readonly IAuthService _authService;

        public WarehouseController(IWarehouseService warehouseService, IProductService productService, IAuthService authService)
        {
            _warehouseService = warehouseService;
            _productService = productService;
            _authService = authService;
        }


        [HttpGet("{warehouseId}/transactions")]
        public async Task<IActionResult> GetAllTransactions(
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

        [Authorize(Roles = "warehouse manager")]
        [HttpPost("{warehouseId}/transactions/sell")]
        public async Task<IActionResult> SellProduct([FromRoute] int warehouseId, [FromQuery] int productId, [FromQuery] int quantity, [FromQuery] Guid customerId)
        {
            string? userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (userId == null)
                return NotFound(new { status = "fail", message = "User not found" });

            var isValidManager = await _authService.CheckIsValidManager(Guid.Parse(userId), warehouseId);
            if (isValidManager.Status)
            {
                var result = await _productService.SellProduct(warehouseId, productId, quantity, customerId);

                if (result.Status)
                    return Ok(new { status = result.Status, message = result.Message });

                return BadRequest(new { status = result.Status, message = result.Message });
            }
            return BadRequest(new { status = "fail", message = isValidManager.Message });
        }

        [Authorize(Roles = "warehouse manager")]
        [HttpPost("{warehouseId}/transactions/purchase")]
        public async Task<IActionResult> PurchaseProduct([FromRoute] int warehouseId, [FromQuery] int productId, [FromQuery] int quantity, [FromQuery] Guid supplierId)
        {
            string? userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (userId == null)
                return NotFound(new { status = "fail", message = "User not found" });

            var isValidManager = await _authService.CheckIsValidManager(Guid.Parse(userId), warehouseId);
            if (isValidManager.Status)
            {
                var result = await _productService.PurchaseProduct(warehouseId, productId, quantity, supplierId);

                if (result.Status)
                    return Ok(new { status = result.Status, message = result.Message });

                return BadRequest(new { status = result.Status, message = result.Message });
            }
            return BadRequest(new { status = "fail", message = isValidManager.Message });
        }

        [HttpGet("{warehouseId}/products")]
        public async Task<IActionResult> GetAllProducts(
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