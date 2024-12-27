using Microsoft.AspNetCore.Mvc;
using StockManager.BLL.DTOs;
using StockManager.BLL.DTOs.Category;
using StockManager.BLL.DTOs.Warehouse;
using StockManager.BLL.Services.CategoryServices;
using StockManager.BLL.Services.WarehouseServices;

namespace StockManager.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IWarehouseService _warehouseService;
        public AdminController(ICategoryService categoryService, IWarehouseService warehouseService)
        {
            _categoryService = categoryService;
            _warehouseService = warehouseService;
        }

        // customer related endpoints

        [HttpGet("customers")]
        public IActionResult GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        [HttpGet("customers/{customerId}")]
        public IActionResult GetCustomer([FromRoute] int customerId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("customers")]
        public IActionResult CreateCustomer([FromBody] CustomerDto customerDto)
        {
            throw new NotImplementedException();
        }

        [HttpPut("customer/{customerId}")]
        public IActionResult UpdateCustomer([FromRoute] int customerId, [FromBody] CustomerDto customerDto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("customer/{customerId}")]
        public IActionResult DeleteCustomer([FromRoute] int customerId, [FromBody] CustomerDto customerDto)
        {
            throw new NotImplementedException();
        }

        // supplier related endpoints

        [HttpGet("suppliers")]
        public IActionResult GetAllSupliers()
        {
            throw new NotImplementedException();
        }

        [HttpGet("suppliers/{supplierId}")]
        public IActionResult GetSupplier([FromRoute] int supplierId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("supplier")]
        public IActionResult CreateSupplier([FromBody] SupplierDto supplierDto)
        {
            throw new NotImplementedException();
        }

        [HttpPut("supplier/{supplierId}")]
        public IActionResult UpdateSupplier([FromRoute] int supplierId, [FromBody] SupplierDto supplierDto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("supplier/{customerId}")]
        public IActionResult DeleteSupplier([FromRoute] int customerId, [FromBody] CustomerDto customerDto)
        {
            throw new NotImplementedException();
        }

        // Warehouse related endpoints

        [HttpGet("warehouses")]
        public async Task<IActionResult> GetAllWarehouses()
        {
            var response = await _warehouseService.GetAllWarehouses();

            if (response.Status)
                return Ok(response.Data);
            return NotFound(new { status = response.Status, message = response.Message });
        }

        [HttpGet("warehouses/{warehouseId}")]
        public async Task<IActionResult> GetWarehouseById([FromRoute] int warehouseId)
        {
            var response = await _warehouseService.GetWarehouseById(warehouseId);

            if (response.Status)
                return Ok(response.Data);
            return NotFound(new { status = response.Status, message = response.Message });
        }

        [HttpPost("warehouses")]
        public async Task<IActionResult> CreateWarehouse([FromBody] addWarehouseDto addWarehouseDto)
        {
            var response = await _warehouseService.CreateWarehouse(addWarehouseDto);

            if (response.Status)
                return Ok(response.Data);
            return NotFound(new { status = response.Status, message = response.Message });
        }

        [HttpPut("warehouses/{warehouseId}")]
        public async Task<IActionResult> UpdateWarehouse([FromRoute] int warehouseId, [FromBody] editWarehouseDto warehouseDto)
        {
            var response = await _warehouseService.UpdateWarehouse(warehouseId, warehouseDto);

            if (response.Status)
                return Ok(response.Data);
            return NotFound(new { status = response.Status, message = response.Message });
        }

        [HttpDelete("warehouses/{warehouseId}")]
        public IActionResult DeleteWarehouse([FromRoute] int warehouseId)
        {
            var response = await _warehouseService.DeleteWarehouse(warehouseId);

            if (response.Status)
                return Ok(response.Data);
            return NotFound(new { status = response.Status, message = response.Message });
        }

        [HttpPost("warehouses/{warehouseId}/assign-manager")]
        public IActionResult AssignManagerToWarehouse([FromRoute] int warehouseId, [FromQuery] int userId)
        {
            throw new NotImplementedException();
        }

        // Categories related endpoints

        [HttpPost("categories")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(categoryDto);

            var response = await _categoryService.AddCategory(categoryDto);

            if (response.Status)
                return Ok(new { result = "success", data = categoryDto });
            return StatusCode(500, new { result = "fail", message = response.Message });
        }

        [HttpPut("categories/{categoryId}")]
        public IActionResult UpdateCategory([FromBody] CategoryDto categoryDto, [FromRoute] int categoryId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("categories/{categoryId}")]
        public IActionResult DeleteCategory([FromRoute] int categoryId)
        {
            throw new NotImplementedException();
        }

        // Subcategories related endpoints

        [HttpPost("categories/{categoryId}/subcategories")]
        public IActionResult CreateSubcategory([FromBody] SubcategoryDto subcategoryDto, [FromRoute] int categoryId)
        {
            throw new NotImplementedException();
        }

        [HttpPut("categories/{categoryId}/subcategories/{subcategoryId}")]
        public IActionResult UpdateSubcategory(
            [FromBody] SubcategoryDto subcategoryDto,
            [FromRoute] int categoryId,
            [FromRoute] int subcategoryId
            )
        {
            throw new NotImplementedException();
        }

        [HttpDelete("categories/{categoryId}/subcategories/{subcategoryId}")]
        public IActionResult DeleteSubcategory([FromRoute] int categoryId, [FromRoute] int subcategoryId)
        {
            throw new NotImplementedException();
        }
    }
}