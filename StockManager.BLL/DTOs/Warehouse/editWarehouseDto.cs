namespace StockManager.BLL.DTOs.Warehouse
{
    public class editWarehouseDto
    {
        public string WarehouseName { get; set; }
        public string WarehouseLocation { get; set; }
        public DateTime UpdatedDateTime { get; set; } = DateTime.Now;
    }
}
