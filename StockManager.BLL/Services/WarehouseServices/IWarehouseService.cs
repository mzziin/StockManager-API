using StockManager.BLL.ApiModels;
using StockManager.BLL.DTOs;
using StockManager.BLL.DTOs.Product;
using StockManager.BLL.DTOs.Warehouse;

namespace StockManager.BLL.Services.WarehouseServices
{
    public interface IWarehouseService
    {
        Task<ResponseModel<List<outWarehouseDto>>> GetAllWarehouses();
        Task<ResponseModel<outWarehouseDto>> GetWarehouseById(int warehouseId);
        Task<ResponseModel<object>> CreateWarehouse(addWarehouseDto addWarehouseDto);
        Task<ResponseModel<object>> UpdateWarehouse(int warehouseId, editWarehouseDto editWarehouseDto);
        Task<ResponseModel<object>> DeleteWarehouse(int warehouseId);
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
        Task<ResponseModel<object>> AssignManager(int warehouseId, Guid userId);
    }
}