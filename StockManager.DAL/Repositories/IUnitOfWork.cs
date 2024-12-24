using StockManager.DAL.Entities;
using StockManager.DAL.Repositories.ProductWarehouseRepositories;

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
        IProductWarehouseRepository ProductWarehouses { get; }
        Task BeginTransaction();
        Task RollBack();
        Task SaveAsync();
        Task Commit();
    }
}