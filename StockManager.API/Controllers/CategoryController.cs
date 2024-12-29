using Microsoft.AspNetCore.Mvc;
using StockManager.BLL.Services.CategoryServices;

namespace StockManager.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _categoryService.GetAllCategories();
            if (response.Status)
            {
                return Ok(new
                {
                    status = response.Status,
                    message = response.Message,
                    data = response.Data
                });
            }
            return NotFound(new
            {
                status = response.Status,
                message = response.Message
            });
        }

        [HttpGet("{categoryId}/subcategories")]
        public async Task<IActionResult> GetAllSubcategories([FromRoute] int categoryId)
        {
            var response = await _categoryService.GetAllSubcategories(categoryId);
            if (response.Status)
            {
                return Ok(new
                {
                    status = response.Status,
                    message = response.Message,
                    data = response.Data
                });
            }
            return NotFound(new
            {
                status = response.Status,
                message = response.Message
            });
        }
    }
}
