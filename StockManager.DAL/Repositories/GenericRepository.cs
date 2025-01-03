﻿

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

        public async Task<IEnumerable<T>> GetAllAsync() => await _db.AsNoTracking().ToListAsync();

        public async Task<T?> GetByIdAsync(int id) => await _db.FindAsync(id);
        public async Task<T?> GetByIdAsync(Guid id) => await _db.FindAsync(id);

        public async Task<T?> GetByExpressionAsync(Expression<Func<T, bool>> expression) => await _db.FirstOrDefaultAsync(expression);

        public async Task<IEnumerable<T>> GetAllByExpressionAsync(Expression<Func<T, bool>> expression) => await _db.AsNoTracking().Where(expression).ToListAsync();

        public async Task<T> InsertAsync(T entity)
        {
            var result = await _db.AddAsync(entity);
            return result.Entity;
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

        public async Task<bool> Delete(Guid id)
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