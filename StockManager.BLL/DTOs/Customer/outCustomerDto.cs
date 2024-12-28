namespace StockManager.BLL.DTOs.Customer
{
    public class outCustomerDto
    {
        public Guid CustomerId { get; set; }
        public required string CustomerName { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
