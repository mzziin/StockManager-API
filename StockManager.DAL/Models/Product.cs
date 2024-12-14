using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManager.DAL.Models
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

        public int SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; } = null!;

        public ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    }
}