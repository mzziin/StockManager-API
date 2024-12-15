using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManager.BLL.ApiModels.ProductModels
{
    public class UpdateProductModel
    {
        [Required]
        [MaxLength(100)]
        public required string ProductName { get; set; }

        [MaxLength(500)]
        public string? ProductDescription { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
