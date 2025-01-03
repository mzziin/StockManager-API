﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManager.DAL.Entities
{
    public class Purchase
    {
        [Key]
        public Guid PurchaseId { get; set; } = Guid.NewGuid();
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }
        public DateTime PurchaseDateTime { get; set; } = DateTime.Now;

        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; } = null!;

        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = null!;

        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}