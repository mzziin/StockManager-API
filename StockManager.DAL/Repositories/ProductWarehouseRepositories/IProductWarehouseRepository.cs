using StockManager.DAL.Entities;

namespace StockManager.DAL.Repositories.ProductWarehouseRepositories
{
    public interface IProductWarehouseRepository : IGenericRepository<ProductWarehouse>
    {
        Task<List<Product>> GetAllProductsFromWarehouse(
            int warehouseId,
            int? subcategoryId,
            string productName,
            int pageIndex,
            int pageSize
            );
        Task<Product?> GetProductFromWarehouse(int productId, int warehouseId);
        Task<bool> IncrementProductQuantity(int productId, int warehouseId, int quantity);
        Task<bool> DecrementProductQuantity(int productId, int warehouseId, int quantity);
        Task<int> GetStockAsync(int productId, int warehouseId);
    }
}
