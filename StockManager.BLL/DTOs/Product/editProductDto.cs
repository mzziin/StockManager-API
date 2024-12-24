using System.ComponentModel.DataAnnotations;

namespace StockManager.BLL.DTOs.Product
{
    public class editProductDto
    {
        [MaxLength(100)]
        public string? ProductName { get; set; }

        [MaxLength(500)]
        public string? ProductDescription { get; set; }
        public decimal? Price { get; set; } = null;
        public int? Quantity { get; set; } = null;
    }
}
