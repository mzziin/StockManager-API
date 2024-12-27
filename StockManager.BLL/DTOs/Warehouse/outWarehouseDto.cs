namespace StockManager.BLL.DTOs.Warehouse
{
    public class outWarehouseDto
    {
        public int WarehouseId { get; set; }
        public required string WarehouseName { get; set; }
        public required string WarehouseLocation { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public bool IsActive { get; set; }
    }
}