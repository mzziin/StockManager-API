using System.ComponentModel.DataAnnotations;

namespace StockManager.BLL.DTOs.Supplier
{
    public class addSupplierDto
    {

        [Required]
        [MaxLength(100)]
        public required string SupplierName { get; set; }

        [Required]
        [MaxLength(20)]
        public required string PhoneNumber { get; set; }
    }
}
