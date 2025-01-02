using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManager.BLL.DTOs.Category;
using StockManager.BLL.DTOs.Customer;
using StockManager.BLL.DTOs.Supplier;
using StockManager.BLL.DTOs.Warehouse;
using StockManager.BLL.Services.CategoryServices;
using StockManager.BLL.Services.CustomerService;
using StockManager.BLL.Services.SupplierService;
using StockManager.BLL.Services.WarehouseServices;

namespace StockManager.API.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IWarehouseService _warehouseService;
        private readonly ICustomerService _customerService;
        private readonly ISupplierService _supplierService;
        public AdminController(
            ICategoryService categoryService,
            IWarehouseService warehouseService,
            ICustomerService customerService,
            ISupplierService supplierService)
        {
            _categoryService = categoryService;
            _warehouseService = warehouseService;
            _customerService = customerService;
            _supplierService = supplierService;
        }

        // customer related endpoints

        [HttpGet("customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var response = await _customerService.GetAllCustomers();
            if (!response.Status)
                return BadRequest(new { status = "fail", message = response.Message });
            return Ok(new { status = "success", data = response.Data });
        }

        [HttpGet("customers/{customerId}")]
        public async Task<IActionResult> GetCustomer([FromRoute] Guid customerId)
        {
            var response = await _customerService.GetCustomerById(customerId);
            if (!response.Status)
                return BadRequest(new { status = "fail", message = response.Message });
            return Ok(new { status = "success", data = response.Data });
        }

        [HttpPost("customers")]
        public async Task<IActionResult> CreateCustomer([FromBody] addCustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(customerDto);

            var response = await _customerService.CreateCustomer(customerDto);
            if (!response.Status)
                return BadRequest(new { status = "fail", message = response.Message });
            return Ok(new { status = "success", data = response.Data });
        }

        [HttpPut("customer/{customerId}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] Guid customerId, [FromBody] editCustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(customerDto);

            var response = await _customerService.UpdateCustomer(customerId, customerDto);
            if (!response.Status)
                return BadRequest(new { status = "fail", message = response.Message });
            return Ok(new { status = "success" });
        }

        [HttpDelete("customer/{customerId}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid customerId)
        {
            var response = await _customerService.DeleteCustomer(customerId);
            if (!response.Status)
                return BadRequest(new { status = "fail", message = response.Message });
            return NoContent();
        }

        // supplier related endpoints

        [HttpGet("suppliers")]
        public async Task<IActionResult> GetAllSupliers()
        {
            var response = await _supplierService.GetAllSuppliers();

            if (!response.Status)
                return BadRequest(new { status = "fail", message = response.Message });
            return Ok(new { status = "success", data = response.Data });
        }

        [HttpGet("suppliers/{supplierId}")]
        public async Task<IActionResult> GetSupplier([FromRoute] Guid supplierId)
        {
            var response = await _supplierService.GetSupplierById(supplierId);

            if (!response.Status)
                return BadRequest(new { status = "fail", message = response.Message });
            return Ok(new { status = "success", data = response.Data });
        }

        [HttpPost("supplier")]
        public async Task<IActionResult> CreateSupplier([FromBody] addSupplierDto supplierDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(supplierDto);

            var response = await _supplierService.CreateSupplier(supplierDto);

            if (!response.Status)
                return BadRequest(new { status = "fail", message = response.Message });
            return Ok(new { status = "success", data = response.Data });
        }

        [HttpPut("supplier/{supplierId}")]
        public async Task<IActionResult> UpdateSupplier([FromRoute] Guid supplierId, [FromBody] editSupplierDto supplierDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(supplierDto);

            var response = await _supplierService.UpdateSupplier(supplierId, supplierDto);

            if (!response.Status)
                return BadRequest(new { status = "fail", message = response.Message });
            return Ok(new { status = "success", message = response.Message });
        }

        [HttpDelete("supplier/{supplierId}")]
        public async Task<IActionResult> DeleteSupplier([FromRoute] Guid supplierId)
        {
            var response = await _supplierService.DeleteSupplier(supplierId);

            if (!response.Status)
                return BadRequest(new { status = "fail", message = response.Message });
            return Ok(new { status = "success", message = response.Message });
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
        public async Task<IActionResult> DeleteWarehouse([FromRoute] int warehouseId)
        {
            var response = await _warehouseService.DeleteWarehouse(warehouseId);

            if (response.Status)
                return Ok(response.Data);
            return NotFound(new { status = response.Status, message = response.Message });
        }

        [HttpPost("warehouses/{warehouseId}/assign-manager")]
        public async Task<IActionResult> AssignManagerToWarehouse([FromRoute] int warehouseId, [FromQuery] Guid userId)
        {
            var response = await _warehouseService.AssignManager(warehouseId, userId);

            if (response.Status)
                return Ok(new { result = "success", message = response.Message });
            return StatusCode(500, new { result = "fail", message = response.Message });
        }

        // Categories related endpoints

        [HttpPost("categories")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(categoryDto);

            var response = await _categoryService.AddCategory(categoryDto);

            if (response.Status)
                return Ok(new { result = "success", data = response.Data });
            return StatusCode(500, new { result = "fail", message = response.Message });
        }

        [HttpPut("categories/{categoryId}")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto categoryDto, [FromRoute] int categoryId)
        {
            if (!ModelState.IsValid)
                return BadRequest(categoryDto);

            var response = await _categoryService.UpdateCategory(categoryId, categoryDto);

            if (response.Status)
                return Ok(new { result = "success" });
            return StatusCode(500, new { result = "fail", message = response.Message });
        }

        [HttpDelete("categories/{categoryId}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int categoryId)
        {
            var response = await _categoryService.DeleteCategory(categoryId);

            if (response.Status)
                return NoContent();
            return StatusCode(500, new { result = "fail", message = response.Message });
        }

        // Subcategories related endpoints

        [HttpPost("subcategories")]
        public async Task<IActionResult> CreateSubcategory([FromBody] SubcategoryDto subcategoryDto, [FromQuery] int categoryId)
        {
            if (!ModelState.IsValid)
                return BadRequest(subcategoryDto);

            var response = await _categoryService.AddSubcategory(subcategoryDto, categoryId);

            if (response.Status)
                return Ok(new { result = "success", data = response.Data });
            return StatusCode(500, new { result = "fail", message = response.Message });

        }

        [HttpPut("subcategories/{subcategoryId}")]
        public async Task<IActionResult> UpdateSubcategory(
            [FromBody] SubcategoryDto subcategoryDto,
            [FromRoute] int subcategoryId
            )
        {
            if (!ModelState.IsValid)
                return BadRequest(subcategoryDto);

            var response = await _categoryService.UpdateSubcategory(subcategoryId, subcategoryDto);

            if (response.Status)
                return Ok(new { result = "success" });
            return StatusCode(500, new { result = "fail", message = response.Message });
        }

        [HttpDelete("subcategories/{subcategoryId}")]
        public async Task<IActionResult> DeleteSubcategory([FromRoute] int subcategoryId)
        {
            var response = await _categoryService.DeleteSubcategory(subcategoryId);

            if (response.Status)
                return NoContent();
            return StatusCode(500, new { result = "fail", message = response.Message });
        }
    }
}