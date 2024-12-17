namespace StockManager.DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> InsertAsync(T entity);
        bool Update(T entity);
        Task<bool> Delete(int id);
    }
}