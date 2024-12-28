using System.ComponentModel.DataAnnotations;

namespace StockManager.BLL.DTOs.Customer
{
    public class editCustomerDto
    {
        [MaxLength(100)]
        public required string CustomerName { get; set; }

        [MaxLength(20)]
        public required string PhoneNumber { get; set; }
    }
}
