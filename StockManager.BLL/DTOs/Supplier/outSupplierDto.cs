namespace StockManager.BLL.DTOs.Supplier
{
    public class outSupplierDto
    {
        public Guid SupplierId { get; set; }
        public required string SupplierName { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
