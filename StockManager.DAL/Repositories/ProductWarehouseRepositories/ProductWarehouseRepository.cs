using Microsoft.EntityFrameworkCore;
using StockManager.DAL.Data;
using StockManager.DAL.Entities;

namespace StockManager.DAL.Repositories.ProductWarehouseRepositories
{
    public class ProductWarehouseRepository : GenericRepository<ProductWarehouse>, IProductWarehouseRepository
    {
        private readonly AppDbContext _context;
        public ProductWarehouseRepository(AppDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Product>> GetAllProductsFromWarehouse(
            int warehouseId,
            int? subcategoryId,
            string productName,
            int pageIndex,
            int pageSize
            )
        {

            var query = _context.Set<ProductWarehouse>().AsNoTracking().AsQueryable();

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
    }
}
