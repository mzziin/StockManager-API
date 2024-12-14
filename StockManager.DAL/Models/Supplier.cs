using System.ComponentModel.DataAnnotations;

namespace StockManager.DAL.Models
{
    public class Supplier
    {
        [Key]
        public Guid SupplierId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public required string SupplierName { get; set; }

        [Required]
        [MaxLength(20)]
        public required string PhoneNumber { get; set; }
    }
}
