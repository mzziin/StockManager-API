using StockManager.BLL.ApiModels;
using StockManager.BLL.DTOs.Supplier;

namespace StockManager.BLL.Services.SupplierService
{
    public interface ISupplierService
    {
        Task<ResponseModel<outSupplierDto>> CreateSupplier(addSupplierDto addSupplierDto);
        Task<ResponseModel<object>> DeleteSupplier(Guid supplierId);
        Task<ResponseModel<List<outSupplierDto>>> GetAllSuppliers();
        Task<ResponseModel<outSupplierDto>> GetSupplierById(Guid suppierId);
        Task<ResponseModel<object>> UpdateSupplier(Guid supplierId, editSupplierDto editSupplierDto);
    }
}