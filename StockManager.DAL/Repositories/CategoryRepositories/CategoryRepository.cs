using Microsoft.EntityFrameworkCore;
using StockManager.DAL.Data;
using StockManager.DAL.Entities;

namespace StockManager.DAL.Repositories.CategoryRepositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Subcategory>> GetAllSubCategories(int categoryId)
        {
            return await _context.Subcategories
                .Where(c => c.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}
