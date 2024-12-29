using System.ComponentModel.DataAnnotations;

namespace StockManager.BLL.DTOs.Category
{
    public class CategoryDto
    {

        [Required]
        [MaxLength(100)]
        public required string CategoryName { get; set; }
    }
}
