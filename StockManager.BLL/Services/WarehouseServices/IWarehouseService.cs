using StockManager.BLL.ApiModels;
using StockManager.BLL.DTOs;
using StockManager.BLL.DTOs.Product;

namespace StockManager.BLL.Services.WarehouseServices
{
    public interface IWarehouseService
    {
        Task<ResponseModel<List<outProductDto>>> GetAllProductsFromWarehouse(
            int warehouseId,
            int? subcategoryId,
            string productName,
            int pageIndex,
            int pageSize
            );
        Task<ResponseModel<List<outTransaction>>> SearchTransactions(
            int warehouseId,
            string transactionType,
            string startDate,
            string endDate,
            int pageIndex,
            int pageSize
            );

        Task<ResponseModel<outTransaction>> GetTransactionById(int warehouseId, Guid transactionId);
    }
}