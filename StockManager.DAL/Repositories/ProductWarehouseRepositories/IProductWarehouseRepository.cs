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
    }
}
