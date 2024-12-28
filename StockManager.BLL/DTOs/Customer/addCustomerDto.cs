using System.ComponentModel.DataAnnotations;

namespace StockManager.BLL.DTOs.Customer
{
    public class addCustomerDto
    {
        [Required]
        [MaxLength(100)]
        public required string CustomerName { get; set; }

        [Required]
        [MaxLength(20)]
        public required string PhoneNumber { get; set; }
    }
}
