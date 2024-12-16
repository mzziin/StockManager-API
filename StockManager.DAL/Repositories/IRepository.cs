namespace StockManager.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<ICollection<T>> GetAll();
        Task<T> Insert(T entity);
        Task<T> Update(T entity);
        Task Delete(int id);
    }
}
