using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManager.DAL.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string ProductName { get; set; }

        [MaxLength(500)]
        public string? ProductDescription { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime UpdatedDateTime { get; set; } = DateTime.Now;

        public int? SubcategoryId { get; set; }
        public Subcategory? Subcategory { get; set; }

        public ICollection<ProductWarehouse> ProductWarehouses { get; set; } = new List<ProductWarehouse>();
        public ICollection<ProductSale> ProductSales { get; set; } = new List<ProductSale>();
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    }
}