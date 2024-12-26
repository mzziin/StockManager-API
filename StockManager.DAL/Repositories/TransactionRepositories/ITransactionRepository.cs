using StockManager.DAL.Entities;

namespace StockManager.DAL.Repositories.TransactionRepositories
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        Task<List<Transaction>?> GetAllTransactions(int warehouseId, string transactionType, string startDate, string endDate, int pageIndex, int pageSize);
    }
}