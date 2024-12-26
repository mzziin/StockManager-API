using Microsoft.EntityFrameworkCore;
using StockManager.DAL.Data;
using StockManager.DAL.Entities;

namespace StockManager.DAL.Repositories.TransactionRepositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly AppDbContext _context;
        protected readonly DbSet<Transaction> _db;
        public TransactionRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _context = appDbContext;
            _db = _context.Set<Transaction>();
        }

        public async Task<List<Transaction>?> GetAllTransactions(int warehouseId, string transactionType, string startDate, string endDate, int pageIndex, int pageSize)
        {
            var query = _db.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(transactionType))
                query = query.Where(t => t.TransactionType == transactionType);

            if (!string.IsNullOrEmpty(startDate))
            {
                DateTime startdateTime = DateTime.Parse(startDate);
                query = query.Where(t => t.TransactionDateTime > startdateTime);
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                DateTime endateTime = DateTime.Parse(endDate);
                query = query.Where(t => t.TransactionDateTime < endateTime);
            }

            return await query
                .Where(t => t.WarehouseId == warehouseId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
