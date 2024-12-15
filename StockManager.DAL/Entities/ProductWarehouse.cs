namespace StockManager.DAL.Entities
{
    public class ProductWarehouse
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = null!;

        public int StockQuantity { get; set; }
    }

}
