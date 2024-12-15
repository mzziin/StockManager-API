using System.ComponentModel.DataAnnotations;

namespace StockManager.DAL.Entities
{
    public class Transaction
    {
        [Key]
        public Guid TransactionId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(20)]
        public required string TransactionType { get; set; }
        public int Quantity { get; set; }
        public DateTime TransactionDateTime { get; set; } = DateTime.Now;

        [Required]
        public Guid RelatedEntityId { get; set; }

        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();

        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = null!;
    }
}
