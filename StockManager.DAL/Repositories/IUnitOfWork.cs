using StockManager.DAL.Entities;

namespace StockManager.DAL.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<Purchase> Purchases { get; }
        IGenericRepository<Sale> Sales { get; }
        IGenericRepository<Subcategory> Subcategories { get; }
        IGenericRepository<Supplier> Suppliers { get; }
        IGenericRepository<Transaction> Transactions { get; }
        IGenericRepository<Warehouse> Warehouses { get; }
        Task BeginTransaction();
        Task RollBack();
        Task Save();
        Task Commit();
    }
}