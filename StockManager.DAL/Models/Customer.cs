using System.ComponentModel.DataAnnotations;

namespace StockManager.DAL.Models
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
    }
}
