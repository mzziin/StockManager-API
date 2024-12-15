using System.ComponentModel.DataAnnotations;

namespace StockManager.DAL.Entities
{
    public class Subcategory
    {
        [Key]
        public int SubcategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string SubcategoryName { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
