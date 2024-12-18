

using Microsoft.EntityFrameworkCore;
using StockManager.DAL.Data;
using System.Linq.Expressions;

namespace StockManager.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        protected readonly DbSet<T> _db;

        public GenericRepository(AppDbContext dbContext)
        {
            _context = dbContext;
            _db = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _db.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _db.FindAsync(id);
        }
        public async Task<T?> GetByExpressionAsync(Expression<Func<T, bool>> expression)
        {
            return await _db.FirstOrDefaultAsync(expression);
        }
        public async Task<IEnumerable<T>> GetAllByExpressionAsync(Expression<Func<T, bool>> expression)
        {
            return await _db.Where(expression).ToListAsync();
        }
        public async Task<bool> InsertAsync(T entity)
        {
            try
            {
                await _db.AddAsync(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool Update(T entity)
        {
            _db.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _db.FindAsync(id);
            if (entity != null)
            {
                _db.Remove(entity);
                return true;
            }
            return false;
        }
    }
}