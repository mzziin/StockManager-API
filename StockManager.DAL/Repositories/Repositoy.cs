
namespace StockManager.DAL.Repositories
{
    public class Repositoy<T> : IRepository<T> where T : class
    {

        public Task<ICollection<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(T entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
