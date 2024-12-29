using System.ComponentModel.DataAnnotations;

namespace StockManager.BLL.DTOs.Category
{
    public class SubcategoryDto
    {
        [Required]
        [MaxLength(100)]
        public required string SubcategoryName { get; set; }
    }
}
