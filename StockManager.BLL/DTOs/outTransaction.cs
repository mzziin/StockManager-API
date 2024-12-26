namespace StockManager.BLL.DTOs
{
    public class outTransaction
    {
        public Guid TransactionId { get; set; }
        public required string TransactionType { get; set; }
        public int Quantity { get; set; }
        public DateTime TransactionDateTime { get; set; }
    }
}
