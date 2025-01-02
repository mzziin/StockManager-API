using System.Linq.Expressions;

namespace StockManager.DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllByExpressionAsync(Expression<Func<T, bool>> expression);
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> GetByExpressionAsync(Expression<Func<T, bool>> expression);
        Task<T> InsertAsync(T entity);
        bool Update(T entity);
        Task<bool> Delete(int id);
        Task<bool> Delete(Guid id);
    }
}