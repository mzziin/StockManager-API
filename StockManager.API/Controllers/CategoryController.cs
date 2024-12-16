using Microsoft.AspNetCore.Mvc;

namespace StockManager.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{categoryId}/subcategories")]
        public IActionResult GetAllSubcategories([FromRoute] int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
