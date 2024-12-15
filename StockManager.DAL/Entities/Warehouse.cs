using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManager.DAL.Entities
{
    public class Warehouse
    {
        [Key]
        public int WarehouseId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string WarehouseName { get; set; }

        [Required]
        [MaxLength(256)]
        public required string WarehouseLocation { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public bool IsActive { get; set; } = true;

        //todo
        public string? WarehouseManagerId { get; set; }
        [ForeignKey("WarehouseManagerId")]
        public IdentityUser? WarehouseManager { get; set; }


        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}