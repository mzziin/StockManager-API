using System.ComponentModel.DataAnnotations;

namespace StockManager.BLL.DTOs.Warehouse
{
    public class addWarehouseDto
    {
        [Required]
        [MaxLength(100)]
        public required string WarehouseName { get; set; }

        [Required]
        [MaxLength(256)]
        public required string WarehouseLocation { get; set; }
    }
}
