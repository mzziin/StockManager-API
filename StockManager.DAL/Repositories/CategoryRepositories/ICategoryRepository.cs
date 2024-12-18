using StockManager.DAL.Entities;

namespace StockManager.DAL.Repositories.CategoryRepositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Subcategory>> GetAllSubCategories(int categoryId);
    }
}
