using System.ComponentModel.DataAnnotations;

namespace StockManager.DAL.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string CategoryName { get; set; }

        public ICollection<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
    }
}