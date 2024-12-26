namespace StockManager.DAL.Entities
{
    public class ProductPurchase
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public Guid PurchaseId { get; set; }
        public Purchase Purchase { get; set; }

        public int Quantity { get; set; }
    }
}
