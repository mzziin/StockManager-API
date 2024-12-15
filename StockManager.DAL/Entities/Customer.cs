using System.ComponentModel.DataAnnotations;

namespace StockManager.DAL.Entities
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public required string CustomerName { get; set; }

        [Required]
        [MaxLength(20)]
        public required string PhoneNumber { get; set; }

        public ICollection<Sale> Customers { get; set; } = new List<Sale>();
    }
}