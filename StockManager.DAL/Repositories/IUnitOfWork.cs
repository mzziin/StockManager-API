using StockManager.DAL.Entities;
using StockManager.DAL.Repositories.ProductWarehouseRepositories;
using StockManager.DAL.Repositories.TransactionRepositories;

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
        IGenericRepository<Warehouse> Warehouses { get; }
        IGenericRepository<ProductSale> ProductSales { get; }
        IGenericRepository<ProductPurchase> ProductPurchases { get; }
        ITransactionRepository Transactions { get; }
        IProductWarehouseRepository ProductWarehouses { get; }

        Task BeginTransactionAsync();
        Task RollBack();
        Task SaveAsync();
        Task CommitAsync();
    }
}