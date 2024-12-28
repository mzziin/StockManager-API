using System.ComponentModel.DataAnnotations;

namespace StockManager.BLL.DTOs.Supplier
{
    public class editSupplierDto
    {
        [MaxLength(100)]
        public required string SupplierName { get; set; }

        [MaxLength(20)]
        public required string PhoneNumber { get; set; }
    }
}
