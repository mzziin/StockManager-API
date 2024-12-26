namespace StockManager.DAL.Entities
{
    public class ProductSale
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public Guid SaleId { get; set; }
        public Sale Sale { get; set; }

        public int Quantity { get; set; } // Additional field in the join table
    }
}
