using StockManager.BLL.ApiModels;
using StockManager.BLL.DTOs.Product;

namespace StockManager.BLL.Services.WarehouseService
{
    public interface IWarehouseService
    {
        Task<ResponseModel<List<outProductDto>>> GetAllProductsFromWarehouse(int warehouseId, int? subcategoryId, string productName, int pageIndex, int pageSize);
    }
}