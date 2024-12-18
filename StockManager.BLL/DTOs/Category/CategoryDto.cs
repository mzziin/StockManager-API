using System.ComponentModel.DataAnnotations;

namespace StockManager.BLL.DTOs.Category
{
    public class CategoryDto
    {

        [Required]
        public required string CategoryName { get; set; }
    }
}
