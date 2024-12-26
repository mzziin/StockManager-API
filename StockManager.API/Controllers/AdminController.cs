using Microsoft.AspNetCore.Mvc;
using StockManager.BLL.DTOs;
using StockManager.BLL.DTOs.Category;
using StockManager.BLL.Services.CategoryServices;

namespace StockManager.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public AdminController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
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
        public IActionResult GetAllWarehouses()
        {
            throw new NotImplementedException();
        }

        [HttpGet("warehouses/{warehouseId}")]
        public IActionResult GetWarehouseById([FromRoute] int warehouseId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("warehouses")]
        public IActionResult CreateWarehouse([FromBody] WarehouseDto warehouseDto)
        {
            throw new NotImplementedException();
        }

        [HttpPut("warehouses/{warehouseId}")]
        public IActionResult UpdateWarehouse([FromRoute] int warehouseId, [FromBody] WarehouseDto warehouseDto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("warehouses/{warehouseId}")]
        public IActionResult DeleteWarehouse([FromRoute] int warehouseId)
        {
            throw new NotImplementedException();
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