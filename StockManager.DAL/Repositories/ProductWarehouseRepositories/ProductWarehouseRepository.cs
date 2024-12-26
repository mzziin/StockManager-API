using Microsoft.EntityFrameworkCore;
using StockManager.DAL.Data;
using StockManager.DAL.Entities;

namespace StockManager.DAL.Repositories.ProductWarehouseRepositories
{
    public class ProductWarehouseRepository : GenericRepository<ProductWarehouse>, IProductWarehouseRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<ProductWarehouse> _productWarehouse;
        public ProductWarehouseRepository(AppDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _productWarehouse = _context.Set<ProductWarehouse>();
        }

        public async Task<List<Product>> GetAllProductsFromWarehouse(
            int warehouseId,
            int? subcategoryId,
            string productName,
            int pageIndex,
            int pageSize
            )
        {
            var query = _productWarehouse.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(productName))
                query = query.Where(p => p.Product.ProductName.Contains(productName.ToLower()));

            if (subcategoryId != null && subcategoryId > 0)
                query = query.Where(p => p.Product.SubcategoryId == subcategoryId);

            return await query
                .Where(w => w.WarehouseId == warehouseId)
                .Select(p => p.Product)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Product?> GetProductFromWarehouse(int productId, int warehouseId)
        {
            return await _productWarehouse
                .AsNoTracking()
                .Where(p => p.ProductId == productId && p.WarehouseId == warehouseId)
                .Select(p => p.Product)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetStockAsync(int productId, int warehouseId)
        {
            return await _productWarehouse
                .AsNoTracking()
                .Where(p => p.ProductId == productId && p.WarehouseId == warehouseId)
                .Select(p => p.StockQuantity)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IncrementProductQuantity(int productId, int warehouseId, int quantity)
        {
            var row = await _productWarehouse.FindAsync(warehouseId, productId);
            if (row is null)
                return false;
            row.Product.Quantity -= quantity;
            return true;
        }

        public async Task<bool> DecrementProductQuantity(int productId, int warehouseId, int quantity)
        {
            var row = await _productWarehouse.FindAsync(warehouseId, productId);
            if (row == null)
                return false;
            row.Product.Quantity += quantity;
            return true;
        }
    }
}
