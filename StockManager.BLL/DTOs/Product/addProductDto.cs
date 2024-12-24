using System.ComponentModel.DataAnnotations;

namespace StockManager.BLL.DTOs.Product
{
    public class addProductDto
    {
        [Required]
        [MaxLength(100)]
        public required string ProductName { get; set; }

        [MaxLength(500)]
        public string? ProductDescription { get; set; }

        [Required]
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 0;

        public int? SubcategoryId { get; set; } = null;
    }
}
