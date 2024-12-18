namespace StockManager.BLL.DTOs.Product
{
    public class outProductDto
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
